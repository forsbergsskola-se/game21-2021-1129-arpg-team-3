using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyStatsBT : MonoBehaviour {
	[SerializeField] protected float health;
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float armour;
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
