using System.Collections;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	private bool triggered;
	public TextMeshProUGUI text;
	public GameObject effect;

	private void Awake() {
		triggered = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (!triggered) {
			if (other.gameObject.CompareTag("Player")) {
				Trigger();
				other.gameObject.GetComponent<SpawnControl>().spawnPosition = transform.position;
				FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerCheckpoint");
				effect.GetComponent<ParticleSystem>().Play();
				StartCoroutine(CheckpointText());
			}
		}
	}

	private void Trigger() {
		triggered = true;
	}

	private IEnumerator CheckpointText() {
		text.text = "CHECKPOINT";
		yield return new WaitForSeconds(3);
		text.text = "";
	}
}