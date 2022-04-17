using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/EnemySO", fileName = "NewEnemy")]
public class EnemySO : ScriptableObject
{
	
	// Scriptable Object to quickly build multiple enemy types.
	
	[SerializeField] protected string enemyName;
	[SerializeField] protected float weaponDamage;
	[SerializeField] protected float enemyHealth;
	[SerializeField] protected float enemyArmor;

	public string EnemyName => enemyName;
	public float WeaponDamage => weaponDamage;
	public float EnemyHealth 
	{
		get => enemyHealth;
		set => enemyHealth = value;
	}
	public float EnemyArmor => enemyArmor;
}
