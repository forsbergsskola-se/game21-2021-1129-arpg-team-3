using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class StartBoss : MonoBehaviour {
	public BossAttack bossAttack;
	private GameObject player;
	public BossHealth bossHealth;
	private bool triggered;

	private void OnTriggerEnter(Collider other) {
		if (!triggered && other.gameObject.CompareTag("Player")) {
			bossAttack.canAttack = true;
			other.gameObject.GetComponent<PlayerProximity>().StopMusic();
			bossHealth.PlayMusic();
			triggered = true;
			Destroy(gameObject);
		}
	}
}