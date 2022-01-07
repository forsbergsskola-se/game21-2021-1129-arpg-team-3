using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	// [SerializeField] float playerMeleeRange;
	// [SerializeField] private float playerAttackDamage;
	private NavMeshAgent agent;
	private Transform target;
	public CursorManagement cursorManagement;
	private PlayerStats playerStats;
	private KeyHolder keyHolder;
	// public GameObject playerWeapon;
	private GroundItem itemPickup;
	public InventoryObjects inventory;
	public InventoryObjects equipment;
	public bool inDialogue;
	// public Key key;
	public Attribute[] attributes;
	public TextMeshProUGUI text;
	public GameObject messageBox;
	public TextMeshProUGUI messageText;
	public GameObject effect;
	public Animator animator;

	private void Awake() 
	{
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		playerStats.InitializePlayerStats();
		keyHolder = GetComponent<KeyHolder>();
	}

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		DialogueReader.OnStartEndDialogue += StartEndDialogue;
		foreach (var t in attributes) {
			t.SetParent(this);
		}

		for (int i = 0; i < equipment.GetSlots.Length; i++)
		{
			equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
			equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
		}
	}
	
	void Update() 
	{
		GetCursorPosition();
		ChangeCursor();
		PlayerInput();
		Interact();
		LevellingCheck();
	}
	private void PlayerInput() {

		if (!Input.GetMouseButtonUp(0) || Camera.main is null)
			return;
		cursorManagement.DeSpawnRallyPoint();
		TargetCheck();
	}
	private void LevellingCheck() {
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

	Ray GetCursorPosition() 
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
		return ray;
	}
	
	private void TargetCheck() 
	{
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) 
		{
			target = hitInfo.collider.transform; //Sets target
			if ((hitInfo.collider.CompareTag("Ground") || 
			     hitInfo.collider.CompareTag("Door") || 
			     hitInfo.collider.CompareTag("Fire")) &&			    
			    !inDialogue)
			{ 
				cursorManagement.SpawnRallyPoint(hitInfo.point);
				MovePlayer(hitInfo.point); //Moves player to point.
			}
			else if (hitInfo.collider.CompareTag("Enemy") || 
			         hitInfo.collider.CompareTag("Key") || 
			         hitInfo.collider.CompareTag("NPC") ||
			         hitInfo.collider.CompareTag("Item")) 
			{
				MoveAttack();
			}
			else 
			{
				if (hitInfo.collider.CompareTag("Invalid")) {
					FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerDeny");
				}
			}
		}
		else 
		{
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	private void MovePlayer(Vector3 point) {
		if (inDialogue)
			return;
		StopAttacking(); 
		animator.SetFloat("Speed", agent.speed);
		Debug.Log(agent.speed);
		agent.stoppingDistance = 0; //resets melee range setting
		agent.SetDestination(point); //moves player to point
		
		FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/MainClick");
	}

	private void MoveAttack() {
		if (!(Vector3.Distance(transform.position, target.position) >= playerStats.MeleeRange))
			return; //only when player is not in melee range of enemy
		agent.SetDestination(target.position);
		agent.stoppingDistance = playerStats.MeleeRange; //stops player before melee range
	}
	private void Interact() 
	{
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) 
		{
			target = hitInfo.collider.transform; //reset target
			if (target.CompareTag("Enemy") ||
			    target.CompareTag("Key") ||
			    target.CompareTag("Door") ||
			    target.CompareTag("Item") ||
			    target.CompareTag("NPC") ||
			    target.CompareTag("NPC1"))
			{
				//Attack WHEN player is in Melee range AND target is set to Enemy OR Destroyable.
				if (Vector3.Distance(transform.position, target.position) <= playerStats.MeleeRange) 
				{
					if (target.CompareTag("Enemy")) 
					{
						//Attack
						StartAttacking();
						if (Input.GetMouseButtonUp(0) && target.CompareTag("Enemy")) 
						{
							transform.Translate(new Vector3(0, 0, 0));
							var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
							// Smoothly rotate towards the target point.
							transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, playerStats.CombatRotationSpeed * Time.deltaTime);
						}
					}
					//item pickup
					else if (target.CompareTag("Item")) 
					{
						itemPickup = target.gameObject.GetComponent<GroundItem>();
						Item item = new Item(itemPickup.item);
						if (inventory.AddItem(item, 1))
						{
							Destroy(itemPickup.gameObject);
							itemPickup = null;
						}
					}
					else if (target.CompareTag("Key")) 
					{
						var holder = GetComponent<KeyHolder>();
						holder.AddKey(target.GetComponent<Key>().GetKeyType());
						itemPickup = target.gameObject.GetComponent<GroundItem>();
						inventory.AddItem(new Item(itemPickup.item), 1);
						Destroy(itemPickup.gameObject);
					}
					else if (Input.GetMouseButtonUp(0) && target.CompareTag("NPC")) {
						messageBox.SetActive(true);
						messageText.text = target.GetComponent<Message>().message;
						Time.timeScale = 0f;
						inDialogue= true;
					}
				}
				if (target is not null && target.CompareTag("Enemy") && target.gameObject.GetComponent<Enemy>().Health <= 0) 
				{
					StopAttacking();
				}
			}
		}
	}
	private void StartAttacking() 
	{
		// attackAnimation.gameObject.SetActive(true);
		// playerModel.gameObject.SetActive(false);
		transform.Translate(new Vector3(0, 0, 0));

	}
	private void StopAttacking() 
	{
		// attackAnimation.gameObject.SetActive(false);
		// playerModel.gameObject.SetActive(true);
	}
	
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
				FMODUnity.RuntimeManager.PlayOneShot("event:/Item/KeyPickup");
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
			else 
			{
				// cursorManagement.CursorChange(8);
			}
		}
		else 
		{
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}

	private void OnBeforeSlotUpdate(InventorySlotS _slot)
	{
		if (_slot.ItemObject == null)
			return;
		switch (_slot.parent.inventory.type)
		{
			case InterfaceType.Inventory:
				break;
			case InterfaceType.Equipment:
				print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, 
					", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
				for (int i = 0; i < _slot.item.buffs.Length; i++)
				{
					for (int j = 0; j < attributes.Length; j++)
					{
						if (attributes[j].type == _slot.item.buffs[i].attribute)
							attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
					}
				}
				break;
			case InterfaceType.Chest:
				break;
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
				print(string.Concat
					("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
				for (int i = 0; i < _slot.item.buffs.Length; i++)
				{
					for (int j = 0; j < attributes.Length; j++)
					{
						if (attributes[j].type == _slot.item.buffs[i].attribute)
							attributes[j].value.AddModifier(_slot.item.buffs[i]);
					}
				}
				break;
			case InterfaceType.Chest:
				break;
		}
	}

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
