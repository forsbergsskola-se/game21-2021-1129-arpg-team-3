using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObjects : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDataBaseObject database;
    public List<InventorySlotS> Container = new List<InventorySlotS>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDataBaseObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Resources/Database.asset", typeof(ItemDataBaseObject));
#else
        database = Resources.Load<ItemDataBaseObject>("Database");
#endif
    }

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

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
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
