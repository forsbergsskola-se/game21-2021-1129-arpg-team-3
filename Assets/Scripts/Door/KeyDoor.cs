using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
    // public GameObject hinge;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
	    // transform.RotateAround(hinge.transform.position, Vector3.up, 87);
        gameObject.SetActive(false);
    }
}
