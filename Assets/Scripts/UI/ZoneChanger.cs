using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChanger : MonoBehaviour 
{
	
	// Ground triggers placed at the threshold of zones to change the sound of the player's footsteps.
	
	public int zone;
	
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			other.gameObject.GetComponent<PlayerStatsLoader>().playerStats.zone = zone;
		}
	}
}
