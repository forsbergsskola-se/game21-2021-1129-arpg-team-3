using System;
using UnityEngine;

public class PlayerStatsLoader : MonoBehaviour 
{
	public PlayerStats playerStats;

	private void Start()
	{
		playerStats._playerController = FindObjectOfType<PlayerController>();
	}
}
