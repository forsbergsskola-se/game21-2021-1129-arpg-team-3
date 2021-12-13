using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageYY : MonoBehaviour
{
    public float DamagePoints = 10f;
    private void OnTriggerStay(Collider other)
    {
        EnemyHealthYY H = other.GetComponent<EnemyHealthYY>();
        if (H==null) return;
        {
            H.HealthPoints -= DamagePoints* Time.deltaTime;
        }
    }
}