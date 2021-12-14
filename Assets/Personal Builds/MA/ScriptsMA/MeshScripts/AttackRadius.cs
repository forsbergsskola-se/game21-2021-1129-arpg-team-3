using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AttackRadius : MonoBehaviour
{
   private List<iDamageMA> iDamageMA = new List<iDamageMA>();
   public int Damage = 10;
   public float AttackDelay = 0.5f;
   
}
