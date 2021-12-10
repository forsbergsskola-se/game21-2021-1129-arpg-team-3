using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyItemOR Object", menuName = "Inventory SystemOR/Items/KeyItemsOR")]
public class KeyItemObjectOR : ItemObjectOR
{
    public void Awake()
    {
        type = ItemTypeOR.KeyItem;
    }
}
