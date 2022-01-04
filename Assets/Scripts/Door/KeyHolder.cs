using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    public event EventHandler OnKeysChanged;
    private List<Key.KeyType> keyList;
    public bool doorUnlocked = false;
    public KeyDoor keyDoorR;
    public KeyDoor keyDoorB;
    public KeyDoor keyDoorG;

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
    private void Update() {
        TryDoor();
    }
    private void TryDoor() {
        if (keyDoorR != null && ContainsKey(keyDoorR.GetKeyType())) {
            doorUnlocked = true; 
            if (ContainsKey(keyDoorR.GetKeyType()) && Input.GetKeyDown(KeyCode.F) && Vector3.Distance(keyDoorR.transform.position, transform.position) <= 15) { 
                keyDoorR.OpenDoor();
                doorUnlocked = false;
            }
        }
        if (keyDoorB!= null && ContainsKey(keyDoorB.GetKeyType())) {
            doorUnlocked = true; 
            if (ContainsKey(keyDoorB.GetKeyType()) && Input.GetKeyDown(KeyCode.F) && Vector3.Distance(keyDoorB.transform.position, transform.position) <= 15) { 
                keyDoorB.OpenDoor();
                doorUnlocked = false;
            }
        }
        if (keyDoorG!= null && ContainsKey(keyDoorG.GetKeyType())) {
            doorUnlocked = true; 
            if (ContainsKey(keyDoorG.GetKeyType()) && Input.GetKeyDown(KeyCode.F) && Vector3.Distance(keyDoorG.transform.position, transform.position) <= 15) { 
                keyDoorG.OpenDoor();
                doorUnlocked = false;
            }
        }
    }
}
