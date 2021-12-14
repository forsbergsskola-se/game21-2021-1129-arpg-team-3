using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AttackRadius : MonoBehaviour
{
   private List<IDamageableMA> Damagesables = new List<IDamageableMA>();
   public int Damage = 10;
   public float AttackDelay = 0.5f;

   public delegate void AttackEvent(IDamageableMA Target);

   public AttackEvent OnAttack;
   private Coroutine attackCoroutine;

   private void OnTriggerEnter(Collider other)
   {
      IDamageableMA damagesable = other.GetComponent<IDamageableMA>();
      if (damagesable != null)
         Damagesables.Add(damagesable);
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
         Damagesables.Remove(damageable);
         if (Damagesables.Count == 0)
         {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
         }
      }
   }

      private IEnumerator Attack()
      {
         WaitForSeconds Wait = new WaitForSeconds(AttackDelay);
         yield return Wait;

         IDamageableMA closestDamageable = null;
         float closestDistance = float.MaxValue;

         while (Damagesables.Count > 0)
         {
            for (int i = 0; i < Damagesables.Count; i++)
            {
               Transform damageableTransform = Damagesables[i].GetTransform();
               float distance = Vector3.Distance(transform.position, damageableTransform.position);

               if (distance < closestDistance)
               {
                  closestDistance = distance;
                  //closestDistance = Damagesables[i];
               }
            }

            if (closestDamageable != null)
            {
               OnAttack?.Invoke(closestDamageable);
               closestDamageable.TakeDamage(Damage);
               
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;
            yield return Wait;
            //Damagesables.RemoveAll(DisableDamageables);
         }

         attackCoroutine = null;
      }

      private bool DisabledDamageables(IDamageableMA Damageable)
      {
         return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
      
      }
}
 
   
