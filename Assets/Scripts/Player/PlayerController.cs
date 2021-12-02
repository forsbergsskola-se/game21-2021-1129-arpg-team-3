using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	// [SerializeField] float playerMeleeRange;
	// [SerializeField] private float playerAttackDamage;
	private NavMeshAgent _agent;
	private Transform _target;
	public CursorManagement cursorManagement;
	private PlayerStats _playerStats;
	private EnemyStats1 _enemyStats1;
	public DestroyObject destroyObject;

	
	private void Start() {
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Awake() {
		_playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		_playerStats.InitializePlayerStats();
		_enemyStats1 = GetComponent<EnemyStats1Loader>().enemyStats1;
		_enemyStats1.InitializeEnemyStats();
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
				_target = hitInfo.collider.transform; //Sets target
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
		_agent.stoppingDistance = 0; //resets melee range setting
		_agent.SetDestination(point); //moves player to point
		Debug.Log("Play MoveSound");
	}
	
	void MoveAttack() {
		if (Vector3.Distance(this.transform.position, _target.position) >= _playerStats.MeleeRange) { //only when player is not in melee range of enemy
			_agent.transform.rotation = this.transform.rotation;
			_agent.destination = _target.position;
			_agent.stoppingDistance = _playerStats.MeleeRange; //stops player before melee range
			Debug.Log("Play MoveSound");
		}
	}
	private void AttackEnemy() {
		if (_target is not null) {
			//Attack WHEN player is in Melee range AND target is set to Enemy OR Destroyable.
			if (Vector3.Distance(this.transform.position, _target.position) <= _playerStats.MeleeRange)
			{
				if (_target.CompareTag("Enemy"))
				{
					_enemyStats1.TakeDamage(_playerStats.WeaponDamage, _target.gameObject);
					Debug.Log("Play AttackSound");
					_target = null; //Forces player to click again to attack
				}
				else if (_target.CompareTag("Destroyable"))
				{
					destroyObject.Kill();
				}
			}
		}
		// else {
		// 	Debug.LogWarning("TARGET IS NULL!");
		// }
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
	
