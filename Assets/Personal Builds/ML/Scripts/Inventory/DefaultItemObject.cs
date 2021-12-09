using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Default object", menuName = "InventorySystem/Items/Default")]
public class DefaultItemObject : InventoryItemObject
{
   public void Awake()
   {
      type = TypeOfItem.Default;
   }
}
