using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfItem
{
    Food,
    Equipment,
    Weapon,
    Potion,
    Default
}

public abstract class InventoryItemObject : ScriptableObject
{
    public Sprite displayImage;
    public GameObject prefab;
    public TypeOfItem type;
    public int baseValue;
    
    [TextArea(15, 20)] 
    public string description;
}
