using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDamage : MonoBehaviour {
	private EnemySkeleton enemySkeleton;

	private void Awake() {
		enemySkeleton = GetComponent<EnemySkeleton>();
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			enemySkeleton.TakeDamage(30);
		}
	}
}
