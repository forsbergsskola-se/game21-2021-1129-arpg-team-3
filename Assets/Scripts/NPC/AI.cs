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
        if (eyes.Seeing == Seeing.Player)
        {
            currentState.Player = player;
            currentState.Seeing = Seeing.Player;
            if (currentState is Attack attack)
            {
                attack.attackScript = attackScript;
            }
        }
        else
        {
            currentState.Seeing = eyes.Seeing;
        }
        currentState = currentState.Process();
        if(currentState == null)
            Debug.LogError("CurrentState.Process() returned null???");
    }
}
