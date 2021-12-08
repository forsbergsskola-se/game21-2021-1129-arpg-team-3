using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] protected float health;
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float armour;
	// [SerializeField] private GameObject sphere;
	// private GameObject useSphere;
	public GameObject damageText;
	public PlayerStats playerStats;
	public Weapons sSword;


	public float Health {
		get => health;
		set {
			health = value;
			health = Mathf.Clamp(health, 0, maxHealth);
		}
	}
	public float MaxHealth => maxHealth;


	private void Start() {
		health = maxHealth;
	}
	private void LateUpdate() {
		if (Health <= 0) {
			KillEnemy();
		}
	}
	
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			TakeDamage();
		}
	}

	public void TakeDamage() {
		float damageReceived = playerStats.WeaponDamage - armour;
		Health -= damageReceived;
		ShowEnemyDamage(damageReceived);
	}
	private void ShowEnemyDamage(float damageReceived) {
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
	private void KillEnemy()
	{
		playerStats.Experience += MaxHealth * sSword.WeaponDamage * playerStats.XPMultiplier;
		gameObject.SetActive(false);
		Debug.Log("Enemy is Dead");
	}
}
