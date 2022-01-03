using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour 
{
	public float maxHealth;
	public GameObject damageText;
	public PlayerStats playerStats;
	public EnemySO enemySo;
	public GameObject smoke;
	public GameObject drop;
	
	private float health;
	private float armor;
	
	public float Health 
	{
		get => health;
		private set {
			health = value;
			health = Mathf.Clamp(health, 0, maxHealth);
		}
	}
	
	private void Awake() 
	{
		maxHealth = enemySo.EnemyHealth;
		Health = enemySo.EnemyHealth;
	}
	private void LateUpdate() 
	{
		if (Health <= 0) {
			KillEnemy();
		}
	}
	
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PlayerWeapon")) 
		{
			TakeDamage();
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/SwordHit");
		}
		else if (other.gameObject.CompareTag("PlayerRange")) 
		{
			TakeDamage();
			FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/EHit");
		}
	}

	public void TakeDamage() 
	{
		float damageReceived = playerStats.WeaponDamage * Random.Range(0.9f, 1f) - enemySo.EnemyArmor;
		Health -= damageReceived;

		ShowEnemyDamage(damageReceived);
	}
	private void ShowEnemyDamage(float damageReceived) 
	{
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
	private void KillEnemy()
	{
		playerStats.Experience += maxHealth * enemySo.WeaponDamage * playerStats.XPMultiplier;
		gameObject.SetActive(false);
		Instantiate(smoke, transform.position, Quaternion.identity);
		RandomDrop();
		Debug.Log("Enemy is Dead");
		Destroy(gameObject);
	}

	private void RandomDrop() 
	{
		var choice = Random.Range(2, 3);
		switch (choice) {
			case <= 1:
				Instantiate(drop, transform.position, quaternion.identity);
				break;
			case >= 1:
				playerStats.Gold += 50;
				break;
		}
	}
}
