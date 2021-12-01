using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOR : MonoBehaviour
{
    public float enemyHealth;
    private float health;
    private bool triggeringPlayer;
    public float movementSpeed;
    public bool aggro;
    public float attackTimer;
    private float _attackTimer;
    private GameObject playerOR;

    private void Start()
    {
        playerOR = GameObject.FindWithTag("PlayerOR");
    }

    void Update() 
    {
        if (aggro)
        {
            FollowPlayer();
        }
        if (triggeringPlayer)
        {
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerOR")
        {
            triggeringPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerOR")
        {
            triggeringPlayer = false;
        }
    }

    public void Attack()
    {
        
    }
    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerOR.transform.position, movementSpeed);
    }
}
