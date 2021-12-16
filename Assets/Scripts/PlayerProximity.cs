using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
//Sound use only
{
	public GameObject threat;
	private FMOD.Studio.EventInstance instance;
	public FMODUnity.EventReference fmodEvent;
	private PlayerStats playerStats;
	private void Start() {
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.start();
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
	}
	private void Update() {
		float distance = Vector3.Distance(transform.position, threat.transform.position);
		instance.setParameterByName("Hp", playerStats.Health);
		instance.setParameterByName("How Far To Enemy", distance);
		Debug.Log(playerStats.Health);
	}

}
