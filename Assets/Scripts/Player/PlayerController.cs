using System.Collections;
using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	// Controls player movement and interactions. WARNING: VERY HACKY CODE AHEAD!
	
	private NavMeshAgent agent;
	private Transform target;
	public CursorManagement cursorManagement;
	private PlayerStats playerStats;
	private KeyHolder keyHolder;
	private GroundItem itemPickup;
	public InventoryObjects inventory;
	public InventoryObjects equipment;
	public bool inDialogue;
	public Attribute[] attributes;
	public TextMeshProUGUI text;
	public GameObject messageBox;
	public TextMeshProUGUI messageText;
	public GameObject effect;
	public Animator animator;
	private EventInstance instance;
	public FMODUnity.EventReference fmodEvent;
	private bool waiting;

	private void Awake() 
	{
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		playerStats.InitializePlayerStats();
		keyHolder = GetComponent<KeyHolder>();
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		instance.start();

	}

	private void Start() 
	{
		agent = GetComponent<NavMeshAgent>();
		DialogueReader.OnStartEndDialogue += StartEndDialogue;
		foreach (var t in attributes) 
		{
			t.SetParent(this);
		}

		for (int i = 0; i < equipment.GetSlots.Length; i++)
		{
			equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
			equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
		}
	}
	// Update loop checks this in order.
	void Update() 
	{
		GetCursorPosition();
		ChangeCursor();
		PlayerInput();
		Interact();
		LevellingCheck();
		animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
		PlayWalkingSound();
	}
	// Changes the sound of player's footsteps depending on terrain and velocity of player.
	private void PlayWalkingSound() 
	{
		if (!waiting) {
			instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
			instance.setParameterByName("velocity", agent.velocity.sqrMagnitude);
			instance.setParameterByName("GroundTyp", playerStats.zone);
			instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
			instance.start();
			instance.release();
			// Hacky method to time the sound to the pace of the character.
			StartCoroutine(DelayWalk());
		}
	}
	private IEnumerator DelayWalk() 
	{
		waiting = true;
		yield return new WaitForSeconds(0.5f);
		waiting = false;
	}
	// Despawns rally point and starts target checking sequence on left click.
	private void PlayerInput() 
	{
		if (!Input.GetMouseButtonUp(0) || Camera.main is null)
			return;
		cursorManagement.DeSpawnRallyPoint();
		TargetCheck();
	}
	// Check for leveling up. Could move method to playerstats.
	private void LevellingCheck() 
	{
		if (playerStats.Experience < playerStats.MaxExperience)
			return;
		playerStats.Experience -= playerStats.MaxExperience;
		playerStats.PlayerLevel++;
		playerStats.MaxExperience *= playerStats.PlayerLevelMultiplier;
		playerStats.WeaponDamage += 5;
		playerStats.MaxHealth += 5;
		playerStats.Health = playerStats.MaxHealth;
		FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerLevelUp");
		effect.GetComponent<ParticleSystem>().Play();
		StartCoroutine(LevelUpText());
	}
	
	// Fires raycast to detect cursor position.
	Ray GetCursorPosition() 
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return ray;
	}
	// Checks tag of selected gameobject.
	private void TargetCheck() 
	{
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) 
		{
			// Sets clicked gameobject as target.
			target = hitInfo.collider.transform;
			if ((hitInfo.collider.CompareTag("Ground") || 
			     hitInfo.collider.CompareTag("Door") || 
			     hitInfo.collider.CompareTag("Fire")) &&			    
			    !inDialogue)
			{ 
				// Player is moved to the exact spot of Ground, Door and Fire tagged gameobjects.
				cursorManagement.SpawnRallyPoint(hitInfo.point);
				MovePlayer(hitInfo.point);
			}
			else if (hitInfo.collider.CompareTag("Enemy") || 
			         hitInfo.collider.CompareTag("Key") || 
			         hitInfo.collider.CompareTag("NPC") ||
			         hitInfo.collider.CompareTag("Item")) 
			{
				// Player moves in a different configuration.
				MoveAttack();
			}
			else 
			{
				// Plays an invalid sound when an invalid location is selected. Does not move or change current movement.
				if (hitInfo.collider.CompareTag("Invalid")) {
					FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerDeny");
				}
			}
		}
		else 
		{
			// Warning for null checks.
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	// Player stops attacking and moves to exact location with 0 stopping distance and sound feedback.
	private void MovePlayer(Vector3 point) 
	{
		if (inDialogue)
			return;
		StopAttacking();
		agent.stoppingDistance = 0;
		agent.SetDestination(point);
		FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/MainClick");
	}
	// Player stops a distance away from the enemy or interactable object. Consider renaming
	private void MoveAttack() 
	{
		// Will only move if the player is beyond melee range.
		if (!(Vector3.Distance(transform.position, target.position) >= playerStats.MeleeRange))
			return;
		agent.SetDestination(target.position);
		// Stops player before melee range with account for jitter.
		agent.stoppingDistance = playerStats.MeleeRange - 0.5f;
	}
	// Allows player to interact or attack. Consider splitting this method
	private void Interact() 
	{
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) 
		{
			target = hitInfo.collider.transform;
			if (target.CompareTag("Enemy") ||
			    target.CompareTag("Key") ||
			    target.CompareTag("Door") ||
			    target.CompareTag("Item") ||
			    target.CompareTag("NPC") ||
			    target.CompareTag("NPC1"))
			{
				// AUTO attack when player is in melee range and target is set to Enemy.
				if (Vector3.Distance(transform.position, target.position) <= playerStats.MeleeRange) 
				{
					if (target.CompareTag("Enemy")) 
					{
						StartAttacking();
						if (Input.GetMouseButtonUp(0) && target.CompareTag("Enemy")) 
						{
							// Allows the rotational adjustment of the player during combat when the enemy is clicked.
							SmoothRotate();
						}
					}
					// Facilitates item pickup with sound.
					else if (target.CompareTag("Item")) 
					{
						itemPickup = target.gameObject.GetComponent<GroundItem>();
						Item item = new Item(itemPickup.item);
						if (inventory.AddItem(item, 1))
						{
							FMODUnity.RuntimeManager.PlayOneShot("event:/Item/PotionPickup");
							Destroy(itemPickup.gameObject);
							itemPickup = null;
						}
					}
					// Facilitates key pickup with sound
					else if (target.CompareTag("Key")) 
					{
						var holder = GetComponent<KeyHolder>();
						holder.AddKey(target.GetComponent<Key>().GetKeyType());
						itemPickup = target.gameObject.GetComponent<GroundItem>();
						inventory.AddItem(new Item(itemPickup.item), 1);
						FMODUnity.RuntimeManager.PlayOneShot("event:/Item/KeyPickup");
						Destroy(itemPickup.gameObject);
					}
					// Reads messages by getting the text from the gameobject and displaying it on the message UI.
					else if (Input.GetMouseButtonUp(0) && target.CompareTag("NPC")) 
					{
						messageBox.SetActive(true);
						messageText.text = target.GetComponent<Message>().message;
						Time.timeScale = 0f;
						inDialogue= true;
					}
				}
				// To prevent a player from attacking a corpse
				if (target is not null && target.CompareTag("Enemy") && target.gameObject.GetComponent<Enemy>().Health <= 0) 
				{
					StopAttacking();
				}
			}
		}
	}
	// Allows the rotational adjustment of the player during combat when the enemy is clicked. Needs a better method.
	private void SmoothRotate() 
	{
		// Adding does not make sense
		// Vector3 lookVector = transform.position + target.transform.position;
		// lookVector.y = transform.position.y;
		// Quaternion rotation = Quaternion.LookRotation(lookVector);
		// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
		
		// Deducted transform.position from target.position
		var playerPosition = transform.position;
		var targetRotation = Quaternion.LookRotation(target.position - playerPosition);
		playerPosition.y = targetRotation.y;
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerStats.CombatRotationSpeed * Time.deltaTime);
		// transform.Translate(new Vector3(0, 0, 0));
	}
	// Controls attack animation.
	private void StartAttacking() 
	{
		animator.SetBool("Attack",true);
		SmoothRotate();
	}
	private void StopAttacking() 
	{
		animator.SetBool("Attack",false);
	}
	// Suggest to move to cursor management script.
	private void ChangeCursor() 
	{
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) 
		{
			var cursorHit = hitInfo.collider;
			if (cursorHit.CompareTag("Ground") || cursorHit.CompareTag("Player") || cursorHit.CompareTag("Fire") || cursorHit.CompareTag("PlayerRange")) 
			{
				cursorManagement.CursorChange(1);
			}
			else if (cursorHit.CompareTag("Enemy") || cursorHit.CompareTag("SSword")) 
			{
				cursorManagement.CursorChange(3);
			}
			else if (cursorHit.CompareTag("Key") || cursorHit.CompareTag("Item")) 
			{
				cursorManagement.CursorChange(4);
			}
			else if (cursorHit.CompareTag("Door") && !keyHolder.doorUnlocked) 
			{
				cursorManagement.CursorChange(6);
			}
			else if (cursorHit.CompareTag("Door") && keyHolder.doorUnlocked) 
			{
				cursorManagement.CursorChange(5);
			}
			else if (cursorHit.CompareTag("NPC") || cursorHit.CompareTag("NPC1")) 
			{
				cursorManagement.CursorChange(7);
			}
			else if (cursorHit.CompareTag("Invalid")) 
			{
				cursorManagement.CursorChange(8);
			}
		}
		else 
		{
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	// Suggestion to move inventory scripts out.
	private void OnBeforeSlotUpdate(InventorySlotS _slot)
	{
		if (_slot.ItemObject == null)
			return;
		switch (_slot.parent.inventory.type)
		{
			case InterfaceType.Inventory:
				break;
			case InterfaceType.Equipment:
				CalculateEquipmentStats(_slot, false);
				break;
			case InterfaceType.Chest:
				break;
		}
	}
	private void CalculateEquipmentStats(InventorySlotS _slot, bool unequipped)
	{
		for (int i = 0; i < _slot.item.buffs.Length; i++)
		{
			for (int j = 0; j < attributes.Length; j++)
			{
				if (attributes[j].type == _slot.item.buffs[i].attribute)
				{
					if (!unequipped)
					{
						attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
						if (attributes[j].type == Attributes.Armor)
						{
							playerStats.PlayerArmour += attributes[j].value.BaseValue;
						}
					}
					else
					{
						attributes[j].value.AddModifier(_slot.item.buffs[i]);
						if (attributes[j].type == Attributes.Armor)
						{
							playerStats.PlayerArmour -= attributes[j].value.BaseValue;
						}
					}
				}
			}
		}
	}
	private void OnAfterSlotUpdate(InventorySlotS _slot)
	{
		if (_slot.ItemObject == null)
			return;
		switch (_slot.parent.inventory.type)
		{
			case InterfaceType.Inventory:
				break;
			case InterfaceType.Equipment:
				CalculateEquipmentStats(_slot, true);
				break;
			case InterfaceType.Chest:
				break;
		}
	}
	// Pauses game when player is in a dialogue.
	private void StartEndDialogue()
	{
		if (!inDialogue)
		{
			inDialogue = true;
			agent.isStopped = true;
			Time.timeScale = 0f;
		}
		else
		{
			inDialogue = false;
			agent.isStopped = false;
			Time.timeScale = 1f;
		}
	}
	// Shows announcement when level up.
	private IEnumerator LevelUpText() 
	{
		text.text = "LEVEL UP!";
		yield return new WaitForSeconds(5);
		text.text = "";
	}
	
	public void AttributeModified(Attribute attribute)
	{
		Debug.Log(string.Concat(attribute.type, " updated. Value is now ", attribute.value._modifiedValue));
	}
}
// Suggestion to move inventory scripts out.
[System.Serializable]
public class Attribute
{
	[System.NonSerialized] public PlayerController parent;
	public Attributes type;
	public ModifiableInt value;

	public void SetParent(PlayerController _parent)
	{
		parent = _parent;
		value = new ModifiableInt(AttributeModified);
	}

	public void AttributeModified()
	{
		parent.AttributeModified(this);
	}
}
