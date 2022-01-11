using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    public event EventHandler OnKeysChanged;
    private List<Key.KeyType> keyList;
    public bool doorUnlocked = false;
    public KeyDoor keyDoorR;
    public KeyDoor keyDoorB;
    public KeyDoor keyDoorG;
    private bool doorOpenA;
    private bool doorOpenB;
    private bool doorOpenC;
    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    public List<Key.KeyType> GetKeyList()
    {
        return keyList;
    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added Key: " + keyType);
        keyList.Add(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void Update() 
    {
        TryDoor();
    }
    private void TryDoor() 
    {
        if (keyDoorR != null && ContainsKey(keyDoorR.GetKeyType())) 
        {
            doorUnlocked = true; 
            if (!doorOpenA && ContainsKey(keyDoorR.GetKeyType()) && Input.GetMouseButtonUp(0) && Vector3.Distance(keyDoorR.transform.position, transform.position) <= 12) 
            { 
                keyDoorR.OpenDoor();
                doorUnlocked = false;
                doorOpenA = true;
            }
        }
        if (keyDoorB!= null && ContainsKey(keyDoorB.GetKeyType())) 
        {
            doorUnlocked = true; 
            if (!doorOpenB && ContainsKey(keyDoorB.GetKeyType()) && Input.GetMouseButtonUp(0) && Vector3.Distance(keyDoorB.transform.position, transform.position) <= 12) 
            { 
                keyDoorB.OpenDoor();
                doorUnlocked = false;
                doorOpenB = true;
            }
        }
        if (keyDoorG!= null && ContainsKey(keyDoorG.GetKeyType())) 
        {
            doorUnlocked = true; 
            if (!doorOpenC && ContainsKey(keyDoorG.GetKeyType()) && Input.GetMouseButtonUp(0) && Vector3.Distance(keyDoorG.transform.position, transform.position) <= 12)
            { 
                keyDoorG.OpenDoor();
                doorUnlocked = false;
                doorOpenC = true;
            }
        }
    }
}
