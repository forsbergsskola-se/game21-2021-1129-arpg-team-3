using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObjects : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemDataBaseObject database;
    public List<InventorySlotS> Container = new List<InventorySlotS>();

    public void AddItem(ItemObject _item, int _amount) 
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlotS(database.GetID[_item], _item, _amount));
    }
    public void RemoveItem(ItemObject item, int amount) 
    {
        // Container.Remove(item);
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++) 
            Container[i].item = database.GetItem[Container[i].iD];
    }
    public void OnBeforeSerialize()
    {
    }
    
}

[System.Serializable]
public class InventorySlotS
{
    public int iD;
    public ItemObject item;
    public int amount;

    public InventorySlotS(int _iD, ItemObject _item, int _amount)
    {
        iD = _iD;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
