// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class EnemyHealthBar : MonoBehaviour {
// 	public Slider slider;
// 	public Gradient gradient;
// 	
// 	public Image _fill;
// 	 PlayerStats _playerStats;
// 	
// 	private void Awake()
// 	{ 
// 		// _playerStats = GetComponentInParent<EnemyStats>();
// 	}
//
// 	private void LateUpdate() {
// 		SetMaxHealth();
// 		ChangeHealthBar();
// 	}
//
// 	private void SetMaxHealth() {
// 		slider.maxValue = 100;
// 		slider.value = _playerStats.Health;
// 		_fill.color = gradient.Evaluate(1f);
// 	}
//
// 	private void ChangeHealthBar() {
// 		slider.value = _playerStats.Health;
// 		_fill.color = gradient.Evaluate(slider.normalizedValue);
// 	}
//
// }