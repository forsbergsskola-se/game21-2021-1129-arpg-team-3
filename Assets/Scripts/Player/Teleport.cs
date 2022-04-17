using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	// Immediately sends the player to the referenced location by respawning.
	
	public GameObject destination;

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			other.gameObject.GetComponent<SpawnControl>().spawnPosition = destination.transform.position;
			other.gameObject.GetComponent<SpawnControl>().Respawn();
		}
	}
}
