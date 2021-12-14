using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageableMA 
{
   void TakeDamage(int Damage);

   Transform GetTransform();
}
