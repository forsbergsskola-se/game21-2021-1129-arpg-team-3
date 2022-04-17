using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolableObjectMA :  MonoBehaviour , IDamageableMA
{
   public AttackRadius attackRadius;
   public EnemyControllerMA movement;
   public NavMeshAgent agent;
   public EnemyScriptableObjectMA enemyScriptableObject;

 private Coroutine lookCoroutine;
   public int health = 100;
  

   private void onAttack(IDamageableMA Target)
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
      agent.enabled = false;
   }

   public virtual void SetupAgentFromConfiguration()
   {
      agent.acceleration = enemyScriptableObject.acceleration;
       agent.angularSpeed = enemyScriptableObject.angularSpeed;
     agent.areaMask = enemyScriptableObject.areMask;
       agent.avoidancePriority = enemyScriptableObject.avoidancePriority;
      agent.baseOffset = enemyScriptableObject.baseOffset;
       agent.height = enemyScriptableObject.height;
      agent.obstacleAvoidanceType = enemyScriptableObject.ObstacleAvoidanceType;
       agent.radius = enemyScriptableObject.radius;
      agent.speed = enemyScriptableObject.speed;
      agent.stoppingDistance = enemyScriptableObject.stoppingDistance;

      //movement.UpdateRate = enemyScriptableObject.aIupdateInterval;

       health = enemyScriptableObject.health;

       //attackRadius.Collider.radius = enemyScriptableObject.attackRadius;
       attackRadius.attackDelay = enemyScriptableObject.attackDelay;
       attackRadius.damage = enemyScriptableObject.damage;
   }

   public void TakeDamage(int Damage)
   {
      health -= Damage;
      if (health <= 0)
      {
         gameObject.SetActive(false);
      }
   }

   public Transform GetTransform()
   {
      return transform;
   }
}
