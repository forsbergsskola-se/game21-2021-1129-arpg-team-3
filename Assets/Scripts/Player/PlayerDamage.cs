using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDamage : MonoBehaviour {

	private PlayerStats playerStats;
	public GameObject damageText;

	private void Awake() {
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SSword")) {
			float damageReceived = other.gameObject.GetComponentInParent<Enemy>().enemySo.WeaponDamage * Random.Range(playerStats.MinDamagePercent, 1f) - playerStats.PlayerArmour;
			ShowPlayerDamage(damageReceived);
		}
	}
	private void ShowPlayerDamage(float damageReceived) {
		playerStats.TakeDamage(damageReceived, gameObject);
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
}
