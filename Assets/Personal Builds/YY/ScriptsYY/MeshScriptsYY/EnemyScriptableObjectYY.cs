using UnityEngine;
using UnityEngine.AI;



[CreateAssetMenu(fileName = "Enemy Configuration",menuName = "ScriptableObject/Enemy Configuration")]
public class EnemyScriptableObjectYY : ScriptableObject
{
    public int health = 100;
    public float attackDelay = 1f;
    public int damage = 5;
    public float attackRadius = 1.5f;
    public float aIupdateInterval = 0.1f;
    
    public float acceleration = 8;
    public float angularSpeed = 120;
    
    public int areMask = -1;
    public int avoidancePriority = 50;
    public float baseOffset = 0;
    public float height = 2f;
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float radius = 0.5f;
    public float speed = 3f;
    public float stoppingDistance = 0.5f;
}

