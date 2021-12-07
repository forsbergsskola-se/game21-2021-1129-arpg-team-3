using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovementYY : MonoBehaviour
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
            RaycastHit hitinfo;


            if (Physics.Raycast(ray, out hitinfo))
            {
                print($"Hit{hitinfo.collider.name}");
                Move(hitinfo.point);
            }
        }
    }
    private void Move(Vector3 point)
    {
        _agent.SetDestination(point);
    }
}