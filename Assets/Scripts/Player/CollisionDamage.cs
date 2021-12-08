using System;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {

	private PlayerStats _playerStats;
	public GameObject damageText;
	public Weapons weapons;

	private void Awake() {
		_playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		_playerStats.InitializePlayerStats();
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SSword")) {
			float damageReceived = weapons.WeaponDamage - _playerStats.PlayerArmour;
			_playerStats.TakeDamage(damageReceived, gameObject);
			ShowPlayerDamage(damageReceived);
		}
	}
	private void ShowPlayerDamage(float damageReceived) {

		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
}
