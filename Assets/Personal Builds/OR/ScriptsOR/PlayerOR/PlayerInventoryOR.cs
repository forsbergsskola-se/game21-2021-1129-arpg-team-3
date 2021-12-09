using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryOR : MonoBehaviour
{
    public InventoryObjectOR inventory;

    // public void OnTriggerEnter(Collider other)
    // {
    //     var item = other.GetComponent<ItemOR>();
    //     if (item)
    //     {
    //         inventory.AddItemOR(item.itemOR, 1);
    //         Destroy(other.gameObject);
    //     }
    // }

    public void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //var item = hit.collider.gameObject.GetComponent<ItemOR>();
         float pickupRange = 2f;
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (Input.GetMouseButtonDown(1))
                {
                   // inventory.AddItemOR(item.itemOR, 1);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.ContainerOR.Clear();
    }
}
