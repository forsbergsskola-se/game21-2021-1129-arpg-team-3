using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using UnityEngine;

public class StartBoss : MonoBehaviour {
	public BossAttack bossAttack;
	private GameObject player;
	public BossHealth bossHealth;
	private bool triggered;
	public GameObject messageBox;
	public TextMeshProUGUI messageText;
	public string message;

	private void OnTriggerEnter(Collider other) {
		if (!triggered && other.gameObject.CompareTag("Player")) {
			bossAttack.canAttack = true;
			other.gameObject.GetComponent<PlayerProximity>().StopMusic();
			bossHealth.PlayMusic();
			triggered = true;
			DisplayMessage();
			Destroy(gameObject);
		}
	}

	private void DisplayMessage() {
		messageBox.SetActive(true);
		messageText.text = message;
		Time.timeScale = 0f;
	}
}