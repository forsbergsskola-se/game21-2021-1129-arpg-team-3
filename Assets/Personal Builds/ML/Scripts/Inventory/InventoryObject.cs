using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new();

    public void AddItem(InventoryItemObject item, int amount)
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

    }
}

[Serializable]
public class InventorySlot
{
    public InventoryItemObject item;
    public int amount;

    public InventorySlot(InventoryItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int amountToAdd)
    {
        amount += amountToAdd;
    }
}
