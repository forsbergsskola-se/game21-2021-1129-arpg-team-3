using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyStats : MonoBehaviour {
	[SerializeField] protected float health = 100;
	[SerializeField] protected float maxHealth = 100;
	[SerializeField] protected float armour = 15;
	private float minHealth = 0;

	private void Awake() {
		health = maxHealth;
	}
	private void LateUpdate() {
		if (health <= 0) {
			gameObject.SetActive(false);
		}
	}
	public void TakeDamage(float damage) {
		health -= damage - armour;
		health = Mathf.Clamp(health, minHealth, maxHealth);
	}
}
