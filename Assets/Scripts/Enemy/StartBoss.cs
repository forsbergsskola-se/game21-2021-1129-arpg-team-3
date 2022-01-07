using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
	public BossAttack BossAttack;
	
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			BossAttack.canAttack = true;
		}
	}
}