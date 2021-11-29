using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementBT : MonoBehaviour {

	[SerializeField] float playerMeleeRange;
	private NavMeshAgent _agent;
	private Transform _target;
	public CursorManagementBT cursorManagementBt;

	private void Start() {
		_agent = GetComponent<NavMeshAgent>();
	}
	void Update() {
		GetCursorPosition();
		if (Input.GetMouseButtonUp(0) & Camera.main is not null) {
			cursorManagementBt.DeSpawnRallyPoint();
			CheckMove();
		}
		ChangeCursor();
		Attack();
	}
	private void Attack() {
		if (_target is not null) {
			//Attack WHEN player is in Melee range AND target is set to Enemy OR Destroyable.
			if (Vector3.Distance(this.transform.position, _target.position) <= playerMeleeRange &&
			    (_target.CompareTag("Enemy") || _target.CompareTag("Destroyable"))) {
				AttackTarget(30);
				_target = null; //Forces player to click again to attack
			}
		}
	}
	private void AttackTarget(float damage) {
		_target.GetComponent<EnemyStatsBT>().TakeDamage(damage);
		Debug.Log("Play AttackSound");
	}
	
	private void ChangeCursor() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			if (hitInfo.collider.CompareTag("Ground")) {
				cursorManagementBt.CursorChange(1);
			}
			else if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("Destroyable")) {
				cursorManagementBt.CursorChange(2);
			}
			else {
				cursorManagementBt.CursorChange(3);
			}
		}
		else {
			Debug.LogWarning("Player RayCast Camera is NULL!");
		}
	}
	private void CheckMove() {
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
			if (hitInfo.collider.CompareTag("Ground")) {
				cursorManagementBt.SpawnRallyPoint(hitInfo.point);
				MovePlayer(hitInfo.point); //Moves player to point.
			}
			else if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("Destroyable")) {
				_target = hitInfo.collider.transform; //Sets target
				MoveAttack();
			}
			else {
				Debug.Log("Play InvalidPosition sound");
				// NoMove(); not needed
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
		if (Vector3.Distance(this.transform.position, _target.position) >= playerMeleeRange) { //only when player is not in melee range of enemy
			_agent.destination = _target.position;
			_agent.stoppingDistance = playerMeleeRange; //stops player before melee range
			Debug.Log("Play MoveSound");
		}
	}
	// void NoMove() { not needed
	// 	
	// }

	Ray GetCursorPosition() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
		return ray;
	}
	
}
