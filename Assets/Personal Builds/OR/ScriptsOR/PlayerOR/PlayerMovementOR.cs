using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementOR : MonoBehaviour
{
    public InventoryObjectOR inventory;
    public float pickupRange = 2f;
    private NavMeshAgent _agent;
    private ItemOR itemPickup;

   private void Start()
   {
       _agent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hitInfo;

           if (Physics.Raycast(ray, out hitInfo))
           {
               print($"Hit {hitInfo.collider.name}");
               itemPickup = hitInfo.collider.gameObject.GetComponent<ItemOR>();
               Move(hitInfo.point);
               
           }
       }
       if (itemPickup)
       {
           Vector3 playerPosition = gameObject.transform.position;
           Vector3 itemPosition = itemPickup.gameObject.transform.position;
           float distanceCheck = Vector3.Distance(playerPosition, itemPosition);
           if (distanceCheck <= pickupRange)
           {
               inventory.AddItemOR(itemPickup.itemOR, 1);
               Destroy(itemPickup.gameObject);
               itemPickup = null;
           }
       }
   }
   private void OnApplicationQuit()
   {
       inventory.ContainerOR.Clear();
   }

   private void Move(Vector3 point)
   {
       _agent.SetDestination(point);
   }
}
