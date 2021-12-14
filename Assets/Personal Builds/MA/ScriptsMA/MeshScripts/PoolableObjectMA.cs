using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolableObjectMA : MonoBehaviour
{
   public EnemyControllerMA Movement;
   public NavMeshAgent Agent;
 //  public EnemyScriptableObject EnemyScriptableObject;
   public int Health = 100;

   public void OnEnable()
   {
      SetupAgentFromConfiguration();
   }

   public void OnDisable()
   {
     // base.OnDisable();
      Agent.enabled = false;
   }

   public virtual void SetupAgentFromConfiguration()
   {
     // Agent.acceleration = EnemyScriptableObject.Accelertation;
     //  Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
     // Agent.areaMask = EnemyScriptableObject.AreaMask;
     //  Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
     // Agent.baseOffset = EnemyScriptableObject.BaseOffset;
     //  Agent.height = EnemyScriptableObject.Height;
     // Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
     //  Agent.radius = EnemyScriptableObject.Radius;
     //  Agent.speed = EnemyScriptableObject.Speed;
     // Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;

     // Movement.UpdateRate = EnemyScriptableObject.AIUpdateInterval;

     //  Health = EnemyScriptableObject.Health;
   }

   public void TakeDamage(int Damage)
   {
      Health -= Damage;
      if (Health <= 0)
      {
         gameObject.SetActive(false);
      }
   }

   public Transform GetTransform()
   {
      return transform;
   }
}
