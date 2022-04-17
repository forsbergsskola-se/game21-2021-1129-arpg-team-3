using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerMA : MonoBehaviour
{
    public GameObject Player;
    public float Distance;
    public bool isAngered;
    public NavMeshAgent _agent;


    
    private void Update()
    {
        Distance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (Distance<=5)
        {
            isAngered = true;
        }

        if (Distance>5f)
        {
            isAngered = false;
        }

        if (isAngered)
        {
            _agent.isStopped = false;
            
            _agent.SetDestination(Player.transform.position);
        }

        if (!isAngered)
        {
            _agent.isStopped = true;
        }
        
    }
}
