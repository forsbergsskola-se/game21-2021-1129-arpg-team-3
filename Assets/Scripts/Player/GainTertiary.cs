using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GainTertiary : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject effect;

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player"))
            return;
        other.GetComponent<PlayerStatsLoader>().playerStats.tertiary = true;
        StartCoroutine(MessageText());
        effect.GetComponent<ParticleSystem>().Play();
    }
    private IEnumerator MessageText() {
        text.text = "RING OF FIRE ENABLED! HIT SPACE TO FIRE!";
        yield return new WaitForSeconds(3);
        text.text = "";
    }
}
