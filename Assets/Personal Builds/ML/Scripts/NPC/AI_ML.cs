using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_ML : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Animator anim = new Animator();
    public Transform player;
    public Vector3 playerPos;
    private State_ML currentState;
    private EnemyAttackScript_ML attackScript;
    private NPCEyes_ML eyes;
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        eyes = GetComponent<NPCEyes_ML>();
        attackScript = GetComponent<EnemyAttackScript_ML>();
    //    currentState = new Idle(gameObject, Agent, anim, player);
    }
    
    void Update()
    {
        if (eyes.SeeingMl == Seeing_ML.Player)
        {
            currentState.Player = player;
            currentState.SeeingMl = Seeing_ML.Player;
           /**
            if (currentState is Attack attack)
            {
            //    attack.attackScript = attackScript;
            }
            **/
        }
        currentState = currentState.Process();
    }
}
