using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class BossHealth : MonoBehaviour {
	private EventInstance instance;
	public FMODUnity.EventReference fmodEvent;
	private float bossHealth;
	private float bossMaxHealth;
	public GameObject theEnd;

	private void Start() 
	{
		bossMaxHealth = GetComponent<Enemy>().maxHealth;
		bossHealth = bossMaxHealth;
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
	}

	private void Update() 
	{
		bossHealth = GetComponent<Enemy>().Health;
		if (bossHealth > 0) 
		{
			float hp = bossHealth / bossMaxHealth * 100;
			instance.setParameterByName("Boss HP", hp);
		}
		else 
		{
			theEnd.SetActive(true);
			instance.stop(STOP_MODE.IMMEDIATE);
			Time.timeScale = 0f;
		}
	}
	public void PlayMusic() 
	{
		instance.start();
	}
}
