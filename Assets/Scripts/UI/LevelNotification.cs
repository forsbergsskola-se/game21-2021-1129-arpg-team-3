using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNotification : MonoBehaviour
{
	public TextMeshProUGUI text;
	private bool triggered;
	public int chapter;

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player") && !triggered) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerRespawn");
			StartCoroutine(MessageText());
			triggered = true;
		}
	}
	private IEnumerator MessageText() 
	{
		text.text = $"Chapter {chapter} Complete!";
		yield return new WaitForSeconds(3);
		text.text = "";
	}
}
