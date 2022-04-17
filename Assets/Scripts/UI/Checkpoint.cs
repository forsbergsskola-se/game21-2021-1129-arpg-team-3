using System.Collections;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour 
{
	
	// Works with SpawnControl.cs to set respawn points.
	
	private bool triggered;
	public TextMeshProUGUI text;

	private void Awake() 
	{
		triggered = false;
	}
	
	// Respawn point is set when player enters it for the first time.
	private void OnTriggerEnter(Collider other) 
	{
		// Entering two or more times is ignored or if entered by a non-player. 
		if (triggered)
			return;
		if (!other.gameObject.CompareTag("Player"))
			return;
		triggered = true;
		other.gameObject.GetComponent<SpawnControl>().spawnPosition = transform.position;
		// Player health replenished, sound plays, announcement text shown.
		other.gameObject.GetComponent<SpawnControl>().ReplenishHealth();
		FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerCheckpoint");
		StartCoroutine(CheckpointText());
	}
	private IEnumerator CheckpointText() 
	{
		text.text = "CHECKPOINT!\nHEALTH RESTORED!";
		yield return new WaitForSeconds(3);
		text.text = "";
	}
}