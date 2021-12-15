using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	public MouseItem mouseItem = new MouseItem();
	// [SerializeField] float playerMeleeRange;
	// [SerializeField] private float playerAttackDamage;
	private NavMeshAgent agent;
	private Transform target;
	public CursorManagement cursorManagement;
	private PlayerStats playerStats;
	public Animator attackAnimation;
	public GameObject playerModel;
	private KeyHolder keyHolder;
	public GameObject playerWeapon;
	private GroundItem itemPickup;
	public InventoryObjects inventory;
	private bool inDialogue;
	private bool cannotAttack = true;
	public Key key;

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		DialogueReader.OnStartEndDialogue += StartEndDialogue;
	}
	
	private void Awake() {
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		playerStats.InitializePlayerStats();
		keyHolder = GetComponent<KeyHolder>();
	}

	private void StartEndDialogue()
	{
		if (!inDialogue)
		{
			inDialogue = true;
			agent.isStopped = true;
		}
		else
		{
			inDialogue = false;
			agent.isStopped = false;
		}
	}
	void Update() {
		GetCursorPosition();
		ChangeCursor();
		if (Input.GetMouseButtonDown(0) & Camera.main is not null) {
			cursorManagement.DeSpawnRallyPoint();
			TargetCheck();
		}
		Interact();
	}
	
	Ray GetCursorPosition() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
		return ray;
	}
	
	private void TargetCheck() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			target = hitInfo.collider.transform; //Sets target
			if ((hitInfo.collider.CompareTag("Ground") || 
			    hitInfo.collider.CompareTag("Key") || 
			    hitInfo.collider.CompareTag("Door") ||
			    hitInfo.collider.CompareTag("Fire") ||
			    hitInfo.collider.CompareTag("Item")) &&
			    !inDialogue)
			{ 
				cursorManagement.SpawnRallyPoint(hitInfo.point);
				MovePlayer(hitInfo.point); //Moves player to point.
			}
			else if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("Key")) {
				MoveAttack();
			}
			else {
				FMODUnity.RuntimeManager.PlayOneShot("event:/Impacts/Destroy Barrel");
			}
		}
		else {
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	private void OnApplicationQuit()
	{
		inventory.Container.Items = new InventorySlotS[28];
	}
	private void MovePlayer(Vector3 point) {
		if (!inDialogue) {
			StopAttacking();
			agent.stoppingDistance = 0; //resets melee range setting
			agent.SetDestination(point); //moves player to point
			FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/MainClick");
		}
	}

	private void MoveAttack() {
		if (Vector3.Distance(this.transform.position, target.position) >= playerStats.MeleeRange) { //only when player is not in melee range of enemy
			agent.SetDestination(target.position);
			agent.stoppingDistance = playerStats.MeleeRange -0.5f; //stops player before melee range
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/SwordSwing");
		}
	}
	private void Interact() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			target = hitInfo.collider.transform; //reset target
			if (target.CompareTag("Enemy") ||
			    target.CompareTag("Key") ||
			    target.CompareTag("Door") ||
			    target.CompareTag("Item") ||
			    target.CompareTag("NPC")) {
				//Attack WHEN player is in Melee range AND target is set to Enemy OR Destroyable.
				if (Vector3.Distance(transform.position, target.position) <= playerStats.MeleeRange + 0.5) {
					if (target.CompareTag("Enemy") || target.CompareTag("Door")) {
						//Attack
						StartAttacking();
						// target = null; //Forces player to click again to attack
						if (Input.GetMouseButtonUp(0) && target.CompareTag("Enemy")) {
							var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
							// Smoothly rotate towards the target point.
							transform.rotation = Quaternion.Slerp(transform.rotation,
								targetRotation, playerStats.CombatRotationSpeed * Time.deltaTime);
						}
					}
					else if (target.CompareTag("Item")) {
						itemPickup = target.gameObject.GetComponent<GroundItem>();
						inventory.AddItem(new Item(itemPickup.item), 1);
						Destroy(itemPickup.gameObject);
						itemPickup = null;
					}
					else if (target.CompareTag("Key")) {
						var holder = GetComponent<KeyHolder>();
						holder.AddKey(key.GetKeyType());
						itemPickup = target.gameObject.GetComponent<GroundItem>();
						inventory.AddItem(new Item(itemPickup.item), 1);
						Destroy(itemPickup.gameObject);
						holder.doorUnlocked = true;
					}
				}
				if (target is not null && target.CompareTag("Enemy") && target.gameObject.GetComponent<Enemy>().Health <= 0) {
					StopAttacking();
				}
			}
		}
	}
	private void StartAttacking() {
		if (cannotAttack) {
			playerWeapon.GetComponent<Collider>().enabled = false;
			StartCoroutine(DelayAttack());
		}
		else {
			attackAnimation.gameObject.SetActive(true);
			playerModel.gameObject.SetActive(false);
			playerWeapon.GetComponent<Collider>().enabled = true;
			transform.Translate(new Vector3(0, 0, 0));		
		}
	}
	private void StopAttacking() {
		attackAnimation.gameObject.SetActive(false);
		playerModel.gameObject.SetActive(true);
	}
	
	private void ChangeCursor() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			var cursorHit = hitInfo.collider;
			if (cursorHit.CompareTag("Ground") || cursorHit.CompareTag("Player") || cursorHit.CompareTag("Fire")) {
				cursorManagement.CursorChange(1);
			}
			else if (cursorHit.CompareTag("Enemy")) {
				cursorManagement.CursorChange(3);
			}
			else if (cursorHit.CompareTag("Key") || cursorHit.CompareTag("Item")) {
				cursorManagement.CursorChange(4);
			}
			else if (cursorHit.CompareTag("Door") && !keyHolder.doorUnlocked) {
				FMODUnity.RuntimeManager.PlayOneShot("event:/Item/KeyPickup");
				cursorManagement.CursorChange(6);
			}
			else if (cursorHit.CompareTag("Door") && keyHolder.doorUnlocked) {
				cursorManagement.CursorChange(5);
			}
			else if (cursorHit.CompareTag("NPC")) {
				cursorManagement.CursorChange(7);
			}
			else {
				cursorManagement.CursorChange(8);
			}
		}
		else {
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	
	public IEnumerator DelayAttack() {
		cannotAttack = false;
		yield return new WaitForSeconds(playerStats.AttackDelay * Time.deltaTime);
		cannotAttack = true;
	}
}
	
