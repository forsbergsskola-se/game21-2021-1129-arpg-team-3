using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyDamage : MonoBehaviour {
	private Enemy enemy;

	private void Awake() {
		enemy = GetComponent<Enemy>();
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			enemy.TakeDamage(30);
		}
	}
}
