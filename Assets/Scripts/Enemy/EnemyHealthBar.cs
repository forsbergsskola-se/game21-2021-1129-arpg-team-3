using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
	[SerializeField] Slider slider;
	[SerializeField] Gradient gradient;
	
	[SerializeField] Image fill;
	private EnemyStats1 enemyStats;
	private AI ai;

	private void Awake() {
		enemyStats = GetComponentInParent<EnemyStats1Loader>().enemyStats1;
		ai = GetComponentInParent<AI>();
	}
	private void Update() {
		transform.rotation = Camera.main.transform.rotation;
	}

	private void LateUpdate() {
		DisableHealthBar();
		Physics.Raycast(GetCursorPosition(), out var hitInfo);
		if (hitInfo.collider.CompareTag("Enemy") || ai.showHealthBar) {
			SetMaxHealth();
			ChangeHealthBar();
		}
		Ray GetCursorPosition() {
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
			return ray;
		}
	}

	private void SetMaxHealth() {
		slider.maxValue = enemyStats.MaxHealth;
		slider.value = enemyStats.Health;
		fill.color = gradient.Evaluate(1f);
	}
	private void ChangeHealthBar() {
		slider.value = enemyStats.Health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
	private void DisableHealthBar() {
		slider.value = 0;
		fill.color = Color.clear;
	}
}
