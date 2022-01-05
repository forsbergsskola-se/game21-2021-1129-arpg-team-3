using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDamage : MonoBehaviour 
{

	private PlayerStats playerStats;
	public GameObject damageText;
	
	[SerializeField] private float timeToTakeDamage;
	private float elapsedTime;


	private void Awake()
	{
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SSword")) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/SwordHit");
			float damageReceived = other.gameObject.GetComponentInParent<Enemy>().enemySo.WeaponDamage * Random.Range(0.9f, 1f) - playerStats.PlayerArmour;
			ShowPlayerDamage(damageReceived);
		}
	}
	private void OnParticleCollision(GameObject other) {
		float damageReceived = 20 * Random.Range(0.9f, 1f) - playerStats.PlayerArmour;
		ShowPlayerDamage(damageReceived);	
	}
	private void ShowPlayerDamage(float damageReceived)
	{
		playerStats.TakeDamage(damageReceived);
		FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerHurt");
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
	// private void OnTriggerStay(Collider other)
	// {
	// 	elapsedTime += Time.deltaTime;
	//
	// 	if (other.gameObject.CompareTag("Fire") && elapsedTime > timeToTakeDamage)
	// 	{
	// 		playerStats.TakeDamage(1);
	// 		elapsedTime = 0;
	// 		ShowPlayerDamage(1);			
	// 	}
	// }
}
