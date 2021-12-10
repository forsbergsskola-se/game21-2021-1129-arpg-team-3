using System;
using System.Collections;
using System.Collections.Generic;
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
	public Animator attackAnimation;
	public GameObject playerModel;
	private KeyHolder keyHolder;
	public GameObject playerWeapon;
	private Item itemPickup;
	public InventoryObjects inventory;
	private bool inDialogue = false;

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
		if (Input.GetMouseButtonUp(0) & Camera.main is not null) {
			cursorManagement.DeSpawnRallyPoint();
			TargetCheck();
		}
		AttackEnemy();
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
				Debug.Log("Play InvalidPosition sound");
			}
		}
		else {
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	private void OnApplicationQuit()
	{
		inventory.Container.Clear();
	}
	private void MovePlayer(Vector3 point) {
		StopAttacking();
		agent.stoppingDistance = 0; //resets melee range setting
		agent.SetDestination(point); //moves player to point
		Debug.Log("Play MoveSound");
	}

	private void MoveAttack() {
		if (Vector3.Distance(this.transform.position, target.position) >= playerStats.MeleeRange) { //only when player is not in melee range of enemy
			StopAttacking();
			agent.SetDestination(target.position);
			agent.stoppingDistance = playerStats.MeleeRange -1; //stops player before melee range
			Debug.Log("Play MoveSound");
		}
	}
	private void AttackEnemy() {
		Physics.Raycast(GetCursorPosition(), out var hitInfo);
		target = hitInfo.collider.transform; //Sets target
		if (target is not null &&
		    (target.CompareTag("Enemy") || 
		     target.CompareTag("Key") || 
		     target.CompareTag("Door") || 
		     target.CompareTag("Item") ||
		     target.CompareTag("NPC"))) {
			//Attack WHEN player is in Melee range AND target is set to Enemy OR Destroyable.
			if (Vector3.Distance(transform.position, target.position) <= playerStats.MeleeRange + 0.5) {
				if (target.CompareTag("Enemy") || target.CompareTag("Key") || target.CompareTag("Door")) {
					StartAttacking();
					Debug.Log("Play AttackSound");
					// target = null; //Forces player to click again to attack
					if (Input.GetMouseButtonUp(0) && target.CompareTag("Enemy")) {
						transform.LookAt(target);
					}
				}
				else if (target.CompareTag("Item"))
				{
					itemPickup = target.gameObject.GetComponent<Item>();
					inventory.AddItem(itemPickup.item, 1); 
					Destroy(itemPickup.gameObject);
					itemPickup = null;
				}
			}
			if (target is not null && target.CompareTag("Enemy") && target.gameObject.GetComponent<Enemy>().Health <= 0) {
				StopAttacking();
			}
		}
	}
	private void StartAttacking() {
		attackAnimation.gameObject.SetActive(true);
		playerModel.gameObject.SetActive(false);
		StartCoroutine(DelayAttack());
	}
	private void StopAttacking() {
		playerWeapon.GetComponent<Collider>().enabled = false;
		attackAnimation.gameObject.SetActive(false);
		playerModel.gameObject.SetActive(true);
	}
	
	private void ChangeCursor() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			if (hitInfo.collider.CompareTag("Ground") || hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Fire")) {
				cursorManagement.CursorChange(1);
			}
			else if (hitInfo.collider.CompareTag("Enemy")) {
				cursorManagement.CursorChange(3);
			}
			else if (hitInfo.collider.CompareTag("Key") || hitInfo.collider.CompareTag("Item")) {
				cursorManagement.CursorChange(4);
			}
			else if (hitInfo.collider.CompareTag("Door") && !keyHolder.doorUnlocked) {
				cursorManagement.CursorChange(6);
			}
			else if (hitInfo.collider.CompareTag("Door") && keyHolder.doorUnlocked) {
				cursorManagement.CursorChange(5);
			}
			else if (hitInfo.collider.CompareTag("NPC")) {
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
		playerWeapon.GetComponent<Collider>().enabled = true;
		yield return new WaitForSeconds(playerStats.AttackDelay);
		playerWeapon.GetComponent<Collider>().enabled = false;
	}
}
	
