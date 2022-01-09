using System.Collections;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour 
{
	private bool triggered;
	public TextMeshProUGUI text;
	// public GameObject effect;

	private void Awake() 
	{
		triggered = false;
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (triggered)
			return;
		if (!other.gameObject.CompareTag("Player"))
			return;
		Trigger();
		other.gameObject.GetComponent<SpawnControl>().spawnPosition = transform.position;
		other.gameObject.GetComponent<SpawnControl>().ReplenishHealth();
		FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerCheckpoint");
		StartCoroutine(CheckpointText());
	}
	private void Trigger() 
	{
		triggered = true;
	}

	private IEnumerator CheckpointText() 
	{
		text.text = "CHECKPOINT!\nHEALTH RESTORED!";
		yield return new WaitForSeconds(3);
		text.text = "";
	}
}