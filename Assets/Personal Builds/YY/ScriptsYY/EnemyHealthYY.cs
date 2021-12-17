using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthYY : MonoBehaviour
{
    public float startingHealth = 100f;
    public GameObject damageText;
    [SerializeField]
    public float healthPoints = 100f;
    
    
    private void Start()
    {
        healthPoints = startingHealth;
    }
}