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
    private bool SetupAttack = true;
    
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
            SetupAttack = true;
        }
        
        if (currentState is Attack attack && SetupAttack)
        {
            SetupAttack = false;
            attack.attackScript = attackScript;
        }
        
        if (eyes.Seeing == Seeing.Nothing)
        {
            SetupAttack = true;
        }
        
        if (currentState is Pursue pursue)
        {
            SetupAttack = true;
        }
        currentState = currentState.Process();
    }
}
