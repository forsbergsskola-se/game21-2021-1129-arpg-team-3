using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AttackRadiusYY : MonoBehaviour
{
   private List<IDamageableYY> Damageables = new List<IDamageableYY>();
   public int damage = 10;
   public float attackDelay = 0.5f;
   public delegate void AttackEvent(IDamageableYY Target);
   public AttackEvent onAttack;
   private Coroutine attackCoroutine;

   private void OnTriggerEnter(Collider other)
   {
      IDamageableYY damageable = other.GetComponent<IDamageableYY>();
      if (damageable != null)
         Damageables.Add(damageable);
      if (attackCoroutine == null)
      {
         attackCoroutine = StartCoroutine(Attack());
      }
   }

   private void OnTriggerExit(Collider other)
   {
      IDamageableYY damageable = other.GetComponent<IDamageableYY>();
      if (damageable != null)
      {
         Damageables.Remove(damageable);
         if (Damageables.Count == 0)
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

         IDamageableYY closestDamageable = null;
         float closestDistance = float.MaxValue;

         while (Damageables.Count > 0)
         {
            for (int i = 0; i < Damageables.Count; i++)
            {
               Transform damageableTransform = Damageables[i].GetTransform();
               float distance = Vector3.Distance(transform.position, damageableTransform.position);

               if (distance < closestDistance)
               {
                  closestDistance = distance;
                  closestDamageable = Damageables[i];
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
            Damageables.RemoveAll(DisabledDamageables);
         }

         attackCoroutine = null;
      }

      private bool DisabledDamageables(IDamageableYY Damageable)
      {
         return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
      
      }
}