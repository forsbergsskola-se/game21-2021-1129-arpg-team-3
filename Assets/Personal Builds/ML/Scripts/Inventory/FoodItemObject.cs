using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Food object", menuName = "InventorySystem/Items/Food")]
public class FoodItemObject : InventoryItemObject
{
    public int restoreHealthValue;
    
    public void Awake()
    {
        type = TypeOfItem.Food;
    }
}
