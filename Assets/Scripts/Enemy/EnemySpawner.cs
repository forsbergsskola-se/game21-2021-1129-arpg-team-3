using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
	
	// Spawns enemies with map markers.
	
	[SerializeField] private GameObject enemyToSpawn;
	[SerializeField] private GameObject spawnPositions;
	private Transform[] _spawnPositions;

	private void Awake()
	{
		// This Spawns 1 enemy at referenced gameobject and each of it's children.
		_spawnPositions = spawnPositions.GetComponentsInChildren<Transform>();

		SpawnObjects();
	}

	private void SpawnObjects()
	{
		for (int i = 0; i < _spawnPositions.Length; i++)
		{
			GameObject enemy = Instantiate(enemyToSpawn, _spawnPositions[i].position, enemyToSpawn.transform.rotation);
			// Unused but available if needed to spawn enemies that have a quest tied to them.
			// enemy.GetComponentInChildren<QuestTarget>().questCode = QuestCode.Kill1;
		}
	}
}
