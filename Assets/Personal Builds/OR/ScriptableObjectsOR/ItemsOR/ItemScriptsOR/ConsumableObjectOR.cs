using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumeable Object", menuName = "Inventory System/Items/Consumeable")]
public class ConsumableObjectOR : ItemObjectOR
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = ItemTypeOR.Consumable;
    }
}
