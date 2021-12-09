using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeOR 
{
    Consumable,
    Equipment,
    KeyItem
}

public abstract class ItemObjectOR : ScriptableObject
{
    public GameObject prefab;
    public ItemTypeOR type;
    [TextArea(15,20)]
    public string description;
    
}
