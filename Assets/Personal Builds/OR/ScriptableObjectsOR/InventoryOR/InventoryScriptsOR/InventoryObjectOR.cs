using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryOR", menuName = "Inventory SystemOR/InventoryOR")]
public class InventoryObjectOR : ScriptableObject
{
    public List<InventorySlotOR> ContainerOR = new List<InventorySlotOR>();

    public void AddItemOR(ItemObjectOR _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < ContainerOR.Count; i++)
        {
            if (ContainerOR[i].item == _item)
            {
                ContainerOR[i].AddAmountOR(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            ContainerOR.Add(new InventorySlotOR(_item, _amount));
        }
    }
}

[System.Serializable]
public class InventorySlotOR
{
    public ItemObjectOR item;
    public int amount;

    public InventorySlotOR(ItemObjectOR _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmountOR(int value)
    {
        amount += value;
    }
}
