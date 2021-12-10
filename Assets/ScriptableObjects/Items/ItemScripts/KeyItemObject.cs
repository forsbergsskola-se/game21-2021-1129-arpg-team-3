using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyItem Object", menuName = "Inventory System/Items/KeyItems")]
public class KeyItemObject : ItemObject
{
    public void Awake()
    {
        type = ItemTypeS.KeyItem;
    }
}
