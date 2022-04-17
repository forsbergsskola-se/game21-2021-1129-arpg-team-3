using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDamage : MonoBehaviour 
{
	// Ways in which the player can take damage
	
	private PlayerStats playerStats;
	public GameObject damageText;
	public GameObject sparks;
	[SerializeField] private float timeToTakeDamage;
	private float elapsedTime;
	
	private void Awake()
	{
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
	}
	private void OnCollisionEnter(Collision other) 
	{
		// Player takes damage when in collision with enemy weapon. Potential for different weapons. Suggestion to move method to enemy instead.
		if (other.gameObject.CompareTag("SSword")) 
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/SwordHit");
			// Player is damaged by a random factor of 90-100% of enemy weapon damage with added damage depending on the player's level after deducting armor.
			float damageReceived = (other.gameObject.GetComponentInParent<Enemy>().enemySo.WeaponDamage + playerStats.PlayerLevel * 5 * playerStats.Vulnerability) * Random.Range(0.9f, 1f) - playerStats.PlayerArmour;
			damageReceived = Mathf.Clamp(damageReceived, 0, 10000);
			playerStats.TakeDamage(damageReceived);
			ShowPlayerDamage(damageReceived);
		}
	}
	// Boss damage since it's the only enemy that attacks with a particle system.
	private void OnParticleCollision(GameObject other) 
	{
		// Vulnerability is also included here to steamline the experience for enemy farmers.
		float damageReceived = (20 + playerStats.PlayerLevel * 5 * playerStats.Vulnerability) * Random.Range(0.9f, 1f) - playerStats.PlayerArmour;
		damageReceived = Mathf.Clamp(damageReceived, 0, 10000);
		playerStats.TakeDamage(damageReceived);
		ShowPlayerDamage(damageReceived);
	}
	// Gives audio and visual feed back with damage numbers when the player takes damage.
	private void ShowPlayerDamage(float damageReceived)
	{
		Instantiate(sparks, transform.position, Quaternion.identity);
		FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerHurt");
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
	
	// Used for fire damage.
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
