using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementOR : MonoBehaviour
{
    private NavMeshAgent _agent;

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
               Move(hitInfo.point);
           }
       }
   }

   private void Move(Vector3 point)
   {
       _agent.SetDestination(point);
   }
}
