using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeS 
{
    Consumable,
    Equipment,
    KeyItem
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemTypeS type;
    [TextArea(15,20)]
    public string description;
    
}
