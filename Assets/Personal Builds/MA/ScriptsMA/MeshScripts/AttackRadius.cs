using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
   private List<IDamageableMA> damageables = new List<IDamageableMA>();
   public int damage = 10;
   public float attackDelay = 0.5f;
   private delegate void AttackEvent(IDamageableMA Target);
   private AttackEvent onAttack;
    private Coroutine attackCoroutine;

   private void OnTriggerEnter(Collider other)
   {
      IDamageableMA damageable = other.GetComponent<IDamageableMA>();
      if (damageable != null)
         damageables.Add(damageable);
      if (attackCoroutine == null)
      {
         attackCoroutine = StartCoroutine(Attack());
      }
   }

   private void OnTriggerExit(Collider other)
   {
      IDamageableMA damageable = other.GetComponent<IDamageableMA>();
      if (damageable != null)
      {
         damageables.Remove(damageable);
         if (damageables.Count == 0)
         {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
         }
      }
   }

      private IEnumerator Attack()
      {
         WaitForSeconds Wait = new WaitForSeconds(attackDelay);
         yield return Wait;

         IDamageableMA closestDamageable = null;
         float closestDistance = float.MaxValue;

         while (damageables.Count > 0)
         {
            for (int i = 0; i < damageables.Count; i++)
            {
               Transform damageableTransform = damageables[i].GetTransform();
               float distance = Vector3.Distance(transform.position, damageableTransform.position);

               if (distance < closestDistance)
               {
                  closestDistance = distance;
                  closestDamageable = damageables[i];
               }
            }

            if (closestDamageable != null)
            {
               onAttack?.Invoke(closestDamageable);
               closestDamageable.TakeDamage(damage);
               
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;
            yield return Wait;
            damageables.RemoveAll(DisabledDamageables);
         }

         attackCoroutine = null;
      }

      private bool DisabledDamageables(IDamageableMA Damageable)
      {
         return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
      
      }

}
 
   
