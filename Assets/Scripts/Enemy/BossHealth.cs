using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class BossHealth : MonoBehaviour {
	public float bossHealth;
	public float bossMaxHealth = 100;
	private EventInstance instance;
	public FMODUnity.EventReference fmodEvent;

	private void Start() {
		bossHealth = bossMaxHealth;
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		instance.start();
	}

	private void Update() {
		if (bossHealth > 0) {
			float hp = bossHealth / bossMaxHealth * 100;
			instance.setParameterByName("Boss HP", hp);
		}
		else {
			instance.stop(STOP_MODE.IMMEDIATE);
		}
	}
}
