using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GainSecondary : MonoBehaviour 
{
	public TextMeshProUGUI text;
	public GameObject effect;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/FireBall");
			other.GetComponent<PlayerStatsLoader>().playerStats.secondary = true;
			StartCoroutine(MessageText());
			effect.GetComponent<ParticleSystem>().Play();
		}
	}
	private IEnumerator MessageText() {
		text.text = "FIREBALL ENABLED! RIGHT CLICK TO FIRE!";
		yield return new WaitForSeconds(3);
		text.text = "";
	}
}
