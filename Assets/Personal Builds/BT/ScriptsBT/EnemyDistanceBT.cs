// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class EnemyDistanceBT : MonoBehaviour
// {	
// 	GameObject enemy;
// 	GameObject player;
// 	TextMeshProUGUI enemyDistance;
// 	private double distanceFromEnemy;
//
// 	private void Awake() {
// 		enemyDistance = GetComponent<TextMeshProUGUI>();
// 	}
//
// 	private void LateUpdate() {
// 		player = GameObject.FindWithTag("Player");
// 		enemy = GameObject.FindWithTag("Enemy");
// 		distanceFromEnemy = Math.Round(Vector3.Distance(player.transform.position, enemy.transform.position));
// 		enemyDistance.text = "Nearest Enemy: " + distanceFromEnemy + "m";
// 	}
// }