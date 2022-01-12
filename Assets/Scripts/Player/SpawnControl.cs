using UnityEngine;
using UnityEngine.AI;

public class SpawnControl : MonoBehaviour 
{
	// Respawns player to last checkpoint when the player dies.
	public Vector3 spawnPosition;
	private PlayerStats playerStats;

	private void Awake() 
	{
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		spawnPosition = transform.position;
	}
	
	public void Respawn() 
	{
		// Disabling NavMeshAgent to phase through obstructions.
		GetComponent<NavMeshAgent>().enabled = false;
		transform.position = spawnPosition; 
		playerStats.Health = playerStats.MaxHealth;
		playerStats.PlayerDied = false;
		GetComponent<NavMeshAgent>().enabled = true;
	}
	// Used by checkpoint.cs. Replenishes health when crossing a checkpoint.
	public void ReplenishHealth()
	{
		playerStats.Health = playerStats.MaxHealth;
	}
}
