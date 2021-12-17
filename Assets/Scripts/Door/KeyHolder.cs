using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    public event EventHandler OnKeysChanged;
    private List<Key.KeyType> keyList;
    public bool doorUnlocked = false;
    public KeyDoor keyDoor;
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

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }
    private void Update() {
        TryDoor();
    }
    private void TryDoor() {
        if (keyDoor != null) {
            if (ContainsKey(keyDoor.GetKeyType())) {
                doorUnlocked = true;
                if (ContainsKey(keyDoor.GetKeyType()) && Input.GetKeyDown(KeyCode.F) && Vector3.Distance(keyDoor.transform.position, transform.position) <= 15) {
                    //holding key to open door #Hodor
                    // RemoveKey(keyDoor.GetKeyType());
                    keyDoor.OpenDoor();
                    //RemoveItem();
                }
            }
        }
    }
}
