using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using UnityEngine;

public class StartBoss : MonoBehaviour {
	public BossAttack bossAttack;
	private GameObject player;
	private BossHealth bossHealth;

	private void Start() 
	{
		bossHealth = GetComponentInParent<BossHealth>();
	}
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			other.gameObject.GetComponent<PlayerProximity>().StopMusic();
			bossHealth.PlayMusic();
			Destroy(gameObject);
			bossAttack.canAttack = true;
		}
	}
}