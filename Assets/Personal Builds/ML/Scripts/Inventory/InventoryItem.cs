using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Potion,
    Weapon,
    Helmet,
    
}

[Serializable]
public class InventoryItem
{
    public ItemType itemType;
    public int Value;
}
