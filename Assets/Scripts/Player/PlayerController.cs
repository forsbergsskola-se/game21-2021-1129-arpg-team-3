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
	public DestroyObject destroyObject;
	public Animator attackAnimation;
	public GameObject playerModel;

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
	}

	private void Awake() {
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		playerStats.InitializePlayerStats();
		// attackAnimation = GetComponentInChildren<Animator>();
	}

	void Update() {
		GetCursorPosition();
		if (Input.GetMouseButtonUp(0) & Camera.main is not null) {
			cursorManagement.DeSpawnRallyPoint();
			TargetCheck();
		}
		ChangeCursor();
		AttackEnemy();
	}
	
	Ray GetCursorPosition() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
		return ray;
	}
	
	private void TargetCheck() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			if (hitInfo.collider.CompareTag("Ground")) {
				cursorManagement.SpawnRallyPoint(hitInfo.point);
				MovePlayer(hitInfo.point); //Moves player to point.
			}
			else if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("Destroyable")) {
				target = hitInfo.collider.transform; //Sets target
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
	void MovePlayer(Vector3 point) {
		agent.stoppingDistance = 0; //resets melee range setting
		agent.SetDestination(point); //moves player to point
		Debug.Log("Play MoveSound");
	}
	
	void MoveAttack() {
		if (Vector3.Distance(this.transform.position, target.position) >= playerStats.MeleeRange) { //only when player is not in melee range of enemy
			agent.destination = target.position;
			agent.stoppingDistance = playerStats.MeleeRange; //stops player before melee range
			Debug.Log("Play MoveSound");
		}
	}
	private void AttackEnemy() {
		if (target is not null) {
			//Attack WHEN player is in Melee range AND target is set to Enemy OR Destroyable.
			if (Vector3.Distance(this.transform.position, target.position) <= playerStats.MeleeRange) {
				transform.LookAt(target);
				if (target.CompareTag("Enemy")) {
					attackAnimation.gameObject.SetActive(true);
					playerModel.gameObject.SetActive(false);
					Debug.Log("Play AttackSound");
					// target = null; //Forces player to click again to attack
				}
				else if (target.CompareTag("Destroyable"))
				{
					destroyObject.Kill();
				}
			}
			else {
				attackAnimation.gameObject.SetActive(false);
				playerModel.gameObject.SetActive(true);
			}
		}

	}
	
	private void ChangeCursor() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			if (hitInfo.collider.CompareTag("Ground") || hitInfo.collider.CompareTag("Player")) {
				cursorManagement.CursorChange(1);
			}
			else if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("Destroyable")) {
				cursorManagement.CursorChange(2);
			}
			else {
				cursorManagement.CursorChange(3);
			}
		}
		else {
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
}
	
