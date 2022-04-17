using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class KeyHolderMA : MonoBehaviour
{
    public event EventHandler OnKeysChanged;
    private List<Key.KeyType> keyList;
    public bool doorUnlocked = false;
    public float minDistance;
    public GameObject door;
    
    
    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F) && Vector3.Distance(door.transform.position, transform.position) <= minDistance) {
            DoorOpen();
        }
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

    private void DoorOpen()
    {
        KeyDoor keyDoor = door.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                //holding key to open door #Hodor
                RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            key.gameObject.SetActive(false);
            doorUnlocked = true;
        }
    }
}