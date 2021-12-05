using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] protected float health;
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float armour;
	private float damageReceived;

	public float Health => health;
	public float MaxHealth => maxHealth;

	public float DamageReceived => damageReceived;

	private void Awake() {
		health = maxHealth;
	}
	private void LateUpdate() {
		if (health <= 0) {
			KillEnemy();
		}
	}

	public void TakeDamage(float damage) {
		damageReceived = damage - armour;
		health -= damageReceived; 
		health = Mathf.Clamp(health, 0, maxHealth);
	}
	private void KillEnemy() {
		gameObject.SetActive(false);
		Debug.Log("Enemy is Dead");
	}
}
