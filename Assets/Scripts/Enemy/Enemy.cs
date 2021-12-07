using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	[SerializeField] protected float health;
	[SerializeField] protected float maxHealth;
	[SerializeField] protected float armour;
	// [SerializeField] private GameObject sphere;
	// private GameObject useSphere;
	public GameObject damageText;


	public float Health {
		get => health;
		set {
			health = value;
			health = Mathf.Clamp(health, 0, maxHealth);
		}
	}
	public float MaxHealth => maxHealth;


	private void Awake() {
		health = maxHealth;
		// useSphere = Instantiate(sphere);
		// useSphere.transform.position = gameObject.transform.position;
		// useSphere.SetActive(false);
	}
	private void LateUpdate() {
		if (Health <= 0) {
			KillEnemy();
		}
	}

	public void TakeDamage(float damage) {
		float damageReceived = damage - armour;
		Health -= damageReceived;
		ShowEnemyDamage(damageReceived);
	}
	private void ShowEnemyDamage(float damageReceived) {
		DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
		indicator.SetDamageText(Convert.ToInt32(damageReceived));
	}
	private void KillEnemy()
	{
		// useSphere.SetActive(true);
		// gameObject.GetComponent<BoxCollider>().isTrigger = true;
		// gameObject.GetComponent<NavMeshObstacle>().enabled = false;
		gameObject.SetActive(false);
		Debug.Log("Enemy is Dead");
	}
}
