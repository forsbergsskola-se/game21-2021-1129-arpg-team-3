using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InventoryHider : MonoBehaviour {
	private Canvas canvas;
	public PlayerController player;
	private void Start() {
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
	}
	private void Update() {
		if (Input.GetKeyUp(KeyCode.I)) {
			canvas.enabled = !canvas.enabled;
			player.inDialogue = !player.inDialogue;
		}
	}
}
