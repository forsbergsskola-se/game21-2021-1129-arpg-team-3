using System;
using UnityEngine;

public class PlayerStatsLoader : MonoBehaviour 
{
	public PlayerStats playerStats;

	private void Awake() {
		playerStats.MaxHealth = 100;
		playerStats.MaxExperience = 50;
		playerStats.Experience = 0;
		playerStats.Gold = 0;
		playerStats.PlayerLevel = 0;
		playerStats.WeaponDamage = 20;
	}
}
