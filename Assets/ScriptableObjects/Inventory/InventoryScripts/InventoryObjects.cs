using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public enum InterfaceType
{
    Inventory,
    Equipment,
    Chest
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObjects : ScriptableObject
{
    public string savePath;
    public ItemDataBaseObject database;
    public InterfaceType type;
    public Inventory Container;
    public InventorySlotS[] GetSlots {get {return Container.Slots;}}
    
    public bool AddItem(Item _item, int _amount) 
    {
        if (EmptySlotCount <= 0)
            return false;
        InventorySlotS slot = FindItemInInventory(_item);
        if (!database.ItemObjects[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }
    public InventorySlotS FindItemInInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id)
                return GetSlots[i];
        }
        return null;
    }
    public InventorySlotS SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].UpdateSlots(_item, _amount);
                return GetSlots[i];
            }
        }
        return null;
    }
    public void SwapItems(InventorySlotS item1, InventorySlotS item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlotS temp = new InventorySlotS(item2.item, item2.amount);
            item2.UpdateSlots(item1.item, item1.amount);
            item1.UpdateSlots(temp.item, temp.amount);
        }
    }
    public void RemoveItem(Item _item) 
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item == _item)
            {
                GetSlots[i].UpdateSlots(null, 0);
            }
        }
    }
    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath),
            FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath),
                FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlots(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}
[System.Serializable]
public class Inventory
{
    public InventorySlotS[] Slots = new InventorySlotS[28];

    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}
public delegate void SlotUpdated(InventorySlotS _slot);

[System.Serializable]
public class InventorySlotS
{
    public ItemTypeS[] AllowedItems = new ItemTypeS[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized] public GameObject slotDisplay;
    [System.NonSerialized] public SlotUpdated OnAfterUpdate;
    [System.NonSerialized] public SlotUpdated OnBeforeUpdate;
    public Item item;
    public int amount;
    public int baseValue;

    public ItemObject ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.Id];
            }
            return null;
        }
    }
    public InventorySlotS()
    {
        UpdateSlots(new Item(), 0);
    }
    public InventorySlotS(Item _item, int _amount)
    {
        UpdateSlots(_item, _amount);
    }
    public void UpdateSlots(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void AddAmount(int value)
    {
        UpdateSlots(item, amount += value);
        
    }

    public void RemoveItem()
    {
        UpdateSlots(new Item(), 0);
    }
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
                return true;
        }

        return false;
    }
}
