using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAnimMaa : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    private string doorOpen = "DoorOpen";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play(doorOpen,0,0.0f);
        }
    }
}
