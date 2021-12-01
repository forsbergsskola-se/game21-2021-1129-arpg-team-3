using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Player/Stats", fileName = "Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float gold;
    [SerializeField] private float score;
    [SerializeField] private float mana;
    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;
    
    
    public float Health {
        get => health;
        set {
            health = value;
            health = Mathf.Clamp(health, minHealth, maxHealth);
        }
    }

    public float Gold {
        get => gold;
        set => gold = value;
    }
    
    public float Mana {
        get => mana;
        set => mana = value;
    }

    public float Score {
        get => score;
        set => score = value;
    }

    public void TakeDamage(float damage, GameObject player) {
        Health -= damage;
        if (Health <= 0) {
            KillPlayer(player);
        }
    }

    public void InitializePlayerStats() {
        health = maxHealth;
    }

    public void KillPlayer(GameObject player) {
        Debug.Log("Player is Dead");
    }
}