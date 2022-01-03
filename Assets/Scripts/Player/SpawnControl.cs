using UnityEngine;

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
		transform.position = spawnPosition; 
		playerStats.InitializePlayerStats();
	}
}
