using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GainSecondary : MonoBehaviour 
{
	public TextMeshProUGUI text;
	public GameObject effect;
	private bool triggered;
	
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player") && !triggered) {
			other.GetComponent<PlayerStatsLoader>().playerStats.secondary = true;
			triggered = true;
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerRespawn");
			StartCoroutine(MessageText());
			effect.GetComponent<ParticleSystem>().Play();
		}
	}
	private IEnumerator MessageText() 
	{
		text.text = "FIREBALL ENABLED!\nRIGHT CLICK TO FIRE!";
		yield return new WaitForSeconds(3);
		text.text = "";
	}
}
