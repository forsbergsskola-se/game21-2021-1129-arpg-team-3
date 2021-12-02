using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
	public Slider slider;
	public Gradient gradient;
	
	public Image fill;
	private EnemyStats1 enemyStats;
	
	private void Awake()
	{
		enemyStats = GetComponentInParent<EnemyStats1Loader>().enemyStats1;
	}
	private void Update() {
		transform.rotation = Camera.main.transform.rotation;
	}

	private void LateUpdate() {
		SetMaxHealth();
		ChangeHealthBar();
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

}
