using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	private bool triggered;

	private void Awake() {
		triggered = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (!triggered) {
			if (other.gameObject.CompareTag("Player")) {
				Trigger();
				other.gameObject.GetComponent<SpawnControl>().spawnPosition = transform.position;
			}
		}
	}

	private void Trigger() {
		triggered = true;
	}
}