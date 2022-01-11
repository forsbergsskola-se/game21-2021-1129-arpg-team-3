using UnityEngine;
using UnityEngine.AI;

public class SpawnControl : MonoBehaviour 
{
	public Vector3 spawnPosition;
	private PlayerStats playerStats;

	private void Awake() 
	{
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
		spawnPosition = transform.position;
	}
	
	public void Respawn() 
	{
		GetComponent<NavMeshAgent>().enabled = false;
		transform.position = spawnPosition; 
		playerStats.Health = playerStats.MaxHealth;
		playerStats.PlayerDied = false;
		GetComponent<NavMeshAgent>().enabled = true;
	}
	public void ReplenishHealth()
	{
		playerStats.Health = playerStats.MaxHealth;
	}
}
