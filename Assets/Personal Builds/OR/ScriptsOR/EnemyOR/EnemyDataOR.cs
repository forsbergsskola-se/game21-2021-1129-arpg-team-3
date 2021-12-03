using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataOR", menuName = "OR Game/Enemy Data")]
public class EnemyDataOR : ScriptableObject
{
   public string enemyName;
   public string description;
   public GameObject enemyModel;
   public int health = 20;
   public float speed = 2f;
   public float detectRange = 10f;
   public int damage = 1;
}
