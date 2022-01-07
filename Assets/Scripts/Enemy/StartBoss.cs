using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class StartBoss : MonoBehaviour {
	public BossAttack BossAttack;
	private GameObject player;
	
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			BossAttack.canAttack = true;
			other.gameObject.GetComponent<PlayerProximity>().StopMusic();
		}
	}
}