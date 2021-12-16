using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",  menuName = "InventorySystem/Inventory")]
public class InventoryObject_ML : ScriptableObject
{
    public List<InventorySlot_ML> container = new();

    public void AddItem(InventoryItemObject_ML item, int amount)
    {
        bool hasItem = false;

        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == item)
            {
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        
        if (!hasItem)
        {
            container.Add(new InventorySlot_ML(item, amount));
        }
    }
}

[Serializable]
public class InventorySlot_ML
{
    public InventoryItemObject_ML item;
    public int amount;

    public InventorySlot_ML(InventoryItemObject_ML _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int amountToAdd)
    {
        amount += amountToAdd;
    }
}
