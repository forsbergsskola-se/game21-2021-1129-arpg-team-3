using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChanger : MonoBehaviour 
{
	public int zone;
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			other.gameObject.GetComponent<PlayerStatsLoader>().playerStats.zone = zone;
		}
	}
}
