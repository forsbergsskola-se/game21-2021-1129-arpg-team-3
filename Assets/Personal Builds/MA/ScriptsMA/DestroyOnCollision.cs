using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.collider.CompareTag("Destroy"))
      {
         Destroy(collision.gameObject);
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Enemy"))
      {
         
      }
   }
}
