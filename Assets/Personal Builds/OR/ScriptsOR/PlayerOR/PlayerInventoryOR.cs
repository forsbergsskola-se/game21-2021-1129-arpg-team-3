using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryOR : MonoBehaviour
{
    public InventoryObjectOR inventory;
    //public float pickupRange = 2f;


    // public void OnTriggerEnter(Collider other)
    // {
    //     var item = other.GetComponent<ItemOR>();
    //     if (item)
    //     {
    //         inventory.AddItemOR(item.itemOR, 1);
    //         Destroy(other.gameObject);
    //     }
    // }

    // public void Update()
    // {
    //     Ray ray = new Ray(transform.position, transform.forward);
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit, pickupRange))
    //     {
    //         var item = hit.collider.gameObject.GetComponent<ItemOR>();
    //         Debug.Log(hit.collider.gameObject);
    //         if (hit.collider.CompareTag("Item"))
    //         {
    //             if (Input.GetMouseButtonDown(1))
    //             {
    //                 
    //                 inventory.AddItemOR(item.itemOR, 1);
    //                 Destroy(hit.collider.gameObject);
    //             }
    //         }
    //     }
    // }

    
}
