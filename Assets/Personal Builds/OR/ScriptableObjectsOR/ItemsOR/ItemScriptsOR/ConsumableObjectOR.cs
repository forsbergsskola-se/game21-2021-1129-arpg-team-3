using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ConsumeableOR Object", menuName = "Inventory SystemOR/Items/ConsumeableOR")]
public class ConsumableObjectOR : ItemObjectOR
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = ItemTypeOR.Consumable;
    }
}
