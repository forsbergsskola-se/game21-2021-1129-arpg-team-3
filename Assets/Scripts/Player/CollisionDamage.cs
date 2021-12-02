using UnityEngine;

public class CollisionDamage : MonoBehaviour {

	[SerializeField] private float SSwordDamage;
	private PlayerStats _playerStats;

	private void Awake() {
		_playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		_playerStats.InitializePlayerStats();
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SSword")) {
			_playerStats.TakeDamage(SSwordDamage, gameObject);
		}
	}
}
