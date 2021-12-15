using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolableObjectMA :  MonoBehaviour , IDamageableMA
{
   public AttackRadius AttackRadius;
   public EnemyControllerMA Movement;
   public NavMeshAgent Agent;
   public EnemyScriptableObjectMA enemyScriptableObject;

 private Coroutine lookCoroutine;
   public int Health = 100;
   private void Awake()
   {
      AttackRadius.OnAttack += OnAttack;
   }

   private void OnAttack(IDamageableMA Target)
   {
        
      if (lookCoroutine != null)
      {
         StopCoroutine(lookCoroutine);
      }

      lookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
   }

   private IEnumerator LookAt(Transform Target)
   {
      Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
      float time = 0;

      while (time < 1)
      {
         transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
         time += Time.deltaTime * 2;
         yield return null;
      }

      transform.rotation = lookRotation;
   }

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
      //Agent.acceleration = enemyScriptableObject.Accelertation;
       Agent.angularSpeed = enemyScriptableObject.AngularSpeed;
     //Agent.areaMask = enemyScriptableObject.AreaMask;
       Agent.avoidancePriority = enemyScriptableObject.AvoidancePriority;
      Agent.baseOffset = enemyScriptableObject.BaseOffset;
       //Agent.height = enemyScriptableObject.Height;
      Agent.obstacleAvoidanceType = enemyScriptableObject.ObstacleAvoidanceType;
       Agent.radius = enemyScriptableObject.Radius;
      Agent.speed = enemyScriptableObject.Speed;
      Agent.stoppingDistance = enemyScriptableObject.StoppingDistance;

      //Movement.UpdateRate = enemyScriptableObject.AIUpdateInterval;

       Health = enemyScriptableObject.Health;

       //AttackRadius.Collider.radius = enemyScriptableObject.AttackRadius;
       AttackRadius.AttackDelay = enemyScriptableObject.AttackDelay;
       AttackRadius.Damage = enemyScriptableObject.Damage;
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
