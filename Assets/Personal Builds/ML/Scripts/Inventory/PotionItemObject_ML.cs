using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatToModify
{
    Health,
    Magic,
    Stamina,
    Strength
}


[CreateAssetMenu(fileName ="New Potion object", menuName = "InventorySystem/Items/Potion")]
public class PotionItemObject_ML : InventoryItemObject_ML
{
    public StatToModify modify;
    public int modifyAmount;

    public void Awake()
    {
        type = TypeOfItem.Potion;
    }
}