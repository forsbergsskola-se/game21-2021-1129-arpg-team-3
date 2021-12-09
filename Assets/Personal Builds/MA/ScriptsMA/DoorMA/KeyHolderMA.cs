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

    private void OnTriggerEnter(Collider collider) //You want to recode to IF Input.KeyDown
    { //&& distance from door
        Ray newRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layerMask = 1 << 7;
        if (Physics.Raycast(newRay,out hit,1))
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (Input.GetKey(KeyCode.F))
                {
                   //(hit.collider.gameObject);

                    hit.collider.gameObject.GetComponent<Door>();
                }
            }
        }
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
            doorUnlocked = true;
        }
        KeyDoorMA keyDoor = collider.GetComponent<KeyDoorMA>(); // Gone
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
    
}
