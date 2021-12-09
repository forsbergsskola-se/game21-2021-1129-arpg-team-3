using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/EnemySO", fileName = "NewEnemy")]
public class EnemySO : ScriptableObject {
	[SerializeField] protected string enemyName;
	[SerializeField] protected float weaponDamage;
	[SerializeField] protected float enemyHealth;
	[SerializeField] protected float enemyArmor;
	[SerializeField] protected float minDamagePercent;

	
	public string EnemyName => enemyName;
	public float WeaponDamage => weaponDamage;
	public float EnemyHealth => enemyHealth;
	public float EnemyArmor => enemyArmor;
	public float MinDamagePercent => minDamagePercent;



}
