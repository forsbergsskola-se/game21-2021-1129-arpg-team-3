// Cecilija Simic Rihtnesberg
//     2021-12-04
    
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaracterAnimatorCSR : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float speed = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("speed", speed);
    }

    
}
