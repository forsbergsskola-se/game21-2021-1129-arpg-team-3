using System.Collections;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	private bool triggered;
	public TextMeshProUGUI text;
	private bool show;

	private void Awake() {
		triggered = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (!triggered) {
			if (other.gameObject.CompareTag("Player")) {
				Trigger();
				other.gameObject.GetComponent<SpawnControl>().spawnPosition = transform.position;
				FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerCheckpoint");
				StartCoroutine(CheckpointText());
			}
		}
	}

	private void Trigger() {
		triggered = true;
	}

	private IEnumerator CheckpointText() {
		show = true;
		text.text = "CHECKPOINT";
		yield return new WaitForSeconds(3);
		show = false;
		text.text = "";
	}
}