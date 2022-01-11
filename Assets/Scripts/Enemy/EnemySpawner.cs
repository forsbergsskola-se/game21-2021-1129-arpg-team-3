using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyToSpawn;
	[SerializeField] private GameObject spawnPositions;
	private Transform[] _spawnPositions;

	private void Awake()
	{
		_spawnPositions = spawnPositions.GetComponentsInChildren<Transform>();

		SpawnObjects();
	}

	private void SpawnObjects()
	{
		for (int i = 0; i < _spawnPositions.Length; i++)
		{
			GameObject enemy = Instantiate(enemyToSpawn, _spawnPositions[i].position, enemyToSpawn.transform.rotation);
			// enemy.GetComponentInChildren<QuestTarget>().questCode = QuestCode.Kill1;
		}
	}
}
