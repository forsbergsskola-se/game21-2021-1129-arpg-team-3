using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour 
{
    public GameObject note;
    public PlayerController player;
    public InventoryHider inventoryHider;


    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I)) 
        {
            note.SetActive(false);
        }
    }
    public void Resume() 
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        note.SetActive(false);
        Time.timeScale = 1f;
        player.inDialogue = false;
        inventoryHider.GetComponent<Canvas>().enabled = false;
    }
}
