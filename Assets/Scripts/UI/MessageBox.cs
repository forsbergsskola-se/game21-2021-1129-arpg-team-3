using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour 
{
    
    // Activates the message UI and replaces the text with the text in Messages.cs.
    
    public GameObject note;
    public PlayerController player;
    public InventoryHider inventoryHider;
    
    private void Update() 
    {
        // Prevents conflicts with menu and inventory.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I)) 
        {
            note.SetActive(false);
        }
    }
    // Closes the note.
    public void Resume() 
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        note.SetActive(false);
        Time.timeScale = 1f;
        player.inDialogue = false;
        inventoryHider.GetComponent<Canvas>().enabled = false;
    }
}
