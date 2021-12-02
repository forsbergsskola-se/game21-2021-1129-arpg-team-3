using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy/Stats1", fileName = "Stats")]
public class EnemyStats1 : ScriptableObject
{
	[SerializeField] private float health;
	[SerializeField] private float gold;
	[SerializeField] private float minHealth;
	[SerializeField] private float maxHealth;
	[SerializeField] private float weaponDamage;
	[SerializeField] private float weaponRange;
	[SerializeField] private float armour;
	
	public float Health {
		get => health;
		set {
			health = value;
			health = Mathf.Clamp(health, minHealth, maxHealth);
		}
	}

	public float Gold {
		get => gold;
		set => gold = value;
	}

	public float WeaponDamage {
		get => weaponDamage;
		set => weaponDamage = value;
	}
    
	public float MeleeRange {
		get => weaponRange;
		set => weaponRange = value;
	}
	
	public float Armour {
		get => armour;
		set => armour = value;
	}

	public void TakeDamage(float damage, GameObject enemy) {
		float damageReceived = damage - armour;
		health -= damageReceived;
		health = Mathf.Clamp(health, minHealth, maxHealth);		
		if (Health <= 0) {
			KillEnemy(enemy);
		}
	}

	public void InitializeEnemyStats() {
		health = maxHealth;
	}

	public void KillEnemy(GameObject enemy) {
		Debug.Log("Enemy is Dead");
	}
}
