using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageReport : MonoBehaviour {
	private TextMesh text;
	private float damageValue;

	private void Awake() {
		text = GetComponent<TextMesh>();
	}
	
	public void TextDisplay(float damageReceived) {
		text.text = "+" + damageReceived;
	}
}
