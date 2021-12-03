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
    private EnemyHealthBar enemyHealthBar;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        eyes = GetComponent<NPCEyes>();
        attackScript = GetComponent<EnemyAttackScript>();
        currentState = new Idle(gameObject, Agent, anim, player);
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        enemyHealthBar.gameObject.SetActive(false);
    }
    
    void Update()
    {
        
        if (eyes.Seeing == Seeing.Player)
        {
            currentState.Player = player;
            currentState.Seeing = Seeing.Player;
            SetupAttack = true;
            enemyHealthBar.gameObject.SetActive(true);
        }
        
        if (currentState is Attack attack && SetupAttack)
        {
            SetupAttack = false;
            attack.attackScript = attackScript;
            enemyHealthBar.gameObject.SetActive(true);
        }
        
        if (eyes.Seeing == Seeing.Nothing && !SetupAttack)
        {
            SetupAttack = true;
            enemyHealthBar.gameObject.SetActive(false);
        }
        
        if (currentState is Pursue pursue && !SetupAttack)
        {
            SetupAttack = true;
            enemyHealthBar.gameObject.SetActive(true);
        }

        currentState = currentState.Process();
    }
}
