using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Weapons", fileName = "Weapon")]
public class Weapons : ScriptableObject {
	[SerializeField] protected string weaponName;
	[SerializeField] protected float weaponDamage;

	public string WeaponName => weaponName;

	public float WeaponDamage => weaponDamage;

}
