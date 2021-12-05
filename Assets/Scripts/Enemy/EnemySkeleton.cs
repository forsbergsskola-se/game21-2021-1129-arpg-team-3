using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour {
	[SerializeField] protected float health;
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float armour;

	public float Health => health;
	public float MaxHealth => maxHealth;

	private void Awake() {
		health = maxHealth;
	}
	private void LateUpdate() {
		if (health <= 0) {
			KillEnemy();
		}
	}

	public void TakeDamage(float damage) {
		float damageReceived = damage - armour;
		health -= damageReceived; 
		health = Mathf.Clamp(health, 0, maxHealth);
	}
	private void KillEnemy() {
		gameObject.SetActive(false);
		Debug.Log("Enemy is Dead");
	}
}
