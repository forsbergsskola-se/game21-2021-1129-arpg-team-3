using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageYY : MonoBehaviour
{
    public EnemyHealthYY enemyHealthYY;
    
    public float damagePoints = 10f;
    private void OnTriggerStay(Collider other)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        float hp = enemyHealthYY.healthPoints;
        hp -= Math.Clamp(damagePoints, 0, hp);
    }
}