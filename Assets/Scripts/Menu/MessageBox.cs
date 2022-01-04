using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour 
{
    public GameObject note;
    public PlayerController player;
    public InventoryHider inventoryHider;
    
    public void Resume() 
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        note.SetActive(false);
        Time.timeScale = 1f;
        player.inDialogue = false;
        inventoryHider.GetComponent<Canvas>().enabled = false;
    }
}
