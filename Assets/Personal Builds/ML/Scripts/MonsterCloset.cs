using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnTrigger
{
    Proximity,
    ManualActivation,
    ManualThenProximity
}

public enum QuestTargetType
{
    EmptyCloset,
    KillIndividual,
    None
}

public class MonsterCloset : MonoBehaviour
{
    [SerializeField] private QuestCode questCode;
    [SerializeField] private int monstersToSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject monsterType;
    [SerializeField] private float triggerRadius = 40;
    [SerializeField] private QuestTargetType questTargetType;
    [SerializeField] private SpawnTrigger spawnTrigger;
   
    private bool canSpawn;
    private int spawnedMonsters;

    public delegate void ClosetEmptyDelegate(QuestCode questCode);
    public static event ClosetEmptyDelegate OnClosetEmpty;
    

    private void SpawnMonster()
    {
        canSpawn = false;
        var enemy = Instantiate(monsterType, transform);
        enemy.GetComponentInChildren<QuestTarget>().questCode = questCode;
        spawnedMonsters++;
        StartCoroutine(DelaySpawn());

        if (spawnedMonsters == monstersToSpawn && questTargetType == QuestTargetType.EmptyCloset)
        {
            TargetHit();
        }
    }

    private void TargetHit()
    {
        if (OnClosetEmpty != null)
        {
            OnClosetEmpty(questCode);
        }
    }
    
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }
    
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && spawnTrigger == SpawnTrigger.Proximity)
        {
            canSpawn = true;
        }
    }

    void Start()
    {
        GetComponent<SphereCollider>().radius = triggerRadius;
        gameObject.tag = "MonsterCloset";
    }
    
    void Update()
    {
        if (canSpawn && spawnedMonsters < monstersToSpawn)
        {
            SpawnMonster();
        }
    }
}
