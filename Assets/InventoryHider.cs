using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHider : MonoBehaviour {
	private Canvas canvas;
	private void Start() {
		canvas = GetComponent<Canvas>();
	}
	private void Update() {
		if (Input.GetKeyUp(KeyCode.I)) {
			canvas.enabled = !canvas.enabled;
		}
	}
}
