using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GainTertiary : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject effect;
    private bool triggered;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && !triggered) 
        {
            other.GetComponent<PlayerStatsLoader>().playerStats.tertiary = true;
            triggered = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerRespawn");
            StartCoroutine(MessageText());
            effect.GetComponent<ParticleSystem>().Play();
        }
    }
    private IEnumerator MessageText() 
    {
        text.text = "RING OF KRISTAL ENABLED!\nHIT SPACE TO FIRE!";
        yield return new WaitForSeconds(3);
        text.text = "";
    }
}
