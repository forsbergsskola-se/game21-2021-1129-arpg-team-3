using System;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {

	private PlayerStats _playerStats;
	public GameObject damageText;
	public Weapons sSword;
	public Weapons bigSword;

	private void Awake() {
		_playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		_playerStats.InitializePlayerStats();
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SSword")) {
			float damageReceived = sSword.WeaponDamage - _playerStats.PlayerArmour;
			ShowPlayerDamage(damageReceived);
		}
		else if (other.gameObject.CompareTag("BigSword")) {
			float damageReceived = bigSword.WeaponDamage - _playerStats.PlayerArmour;
			ShowPlayerDamage(damageReceived);
		}
	}
	private void ShowPlayerDamage(float damageReceived) {
		_playerStats.TakeDamage(damageReceived, gameObject);
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
}
