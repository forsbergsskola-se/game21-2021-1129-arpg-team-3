using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[CreateAssetMenu(fileName = "Enemy Configuration",menuName = "ScriptableObject/Enemy Configuration")]
public class EnemyScriptableObjectMA : ScriptableObject
{
    public int Health = 100;
    public float AttackDelay = 1f;
    public int Damage = 5;
    public float AttackRadius = 1.5f;
    public float AIupdateInterval = 0.1f;
    
    public float Acceleration = 8;
    public float AngularSpeed = 120;
    
    public int AreMask = -1;
    public int AvoidancePriority = 50;
    public float BaseOffset = 0;
    public float height = 2f;
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float Radius = 0.5f;
    public float Speed = 3f;
    public float StoppingDistance = 0.5f;
}
