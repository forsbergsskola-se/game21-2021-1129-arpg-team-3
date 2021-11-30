using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Animator anim = new Animator();
    public Transform player;
    private State currentState;
    private EnemyAttackScript attackScript;
    private NPCEyes eyes;
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        eyes = GetComponent<NPCEyes>();
        attackScript = GetComponent<EnemyAttackScript>();
        currentState = new Idle(gameObject, Agent, anim, player);
    }
    
    void Update()
    {
        if (eyes.seeing == Seeing.Player)
        {
            currentState.Player = player;
            currentState.seeing = Seeing.Player;
            if (currentState is Attack attack)
            {
                attack.attackScript = attackScript;
            }
        }
        else
        {
            currentState.seeing = eyes.seeing;
        }
        currentState = currentState.Process();
    }
}
