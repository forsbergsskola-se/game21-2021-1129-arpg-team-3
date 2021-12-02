using UnityEngine;

public class PlayerDamage : MonoBehaviour {
	private PlayerStats _playerStats;

	private void Awake() {
		_playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		_playerStats.InitializePlayerStats();
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SSword")) {
			_playerStats.TakeDamage(10, gameObject);
		}
	}
}
