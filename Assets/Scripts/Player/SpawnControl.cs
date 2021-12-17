using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour {
	public Vector3 spawnPosition;
	private PlayerStats playerStats;

	private void Awake() {
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		spawnPosition = transform.position;
	}

	private void Update() {
		if (playerStats.PlayerDied) {
			transform.position = spawnPosition;
			playerStats.Health = playerStats.MaxHealth;
			playerStats.PlayerDied = false;
		}
	}
}
