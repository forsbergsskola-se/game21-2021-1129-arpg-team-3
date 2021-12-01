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
    private bool SetupAttack = false;
    
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
        if (eyes.SeeingMl == Seeing.Player)
        {
            currentState.SeeingMl = Seeing.Player;
            SetupAttack = true;
        }
        
        if (eyes.SeeingMl == Seeing.Nothing)
        {
            SetupAttack = true;
        }


        if (currentState is Attack attack)
        {
            if (SetupAttack)
            {
                SetupAttack = false;
                attack.attackScript = attackScript;
            }
        }
        
        if (currentState is Pursue pursue)
        {
            SetupAttack = true;
        }
        
        
        currentState = currentState.Process();
    }
}
