using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneMA : MonoBehaviour
{
    public float DamagePoints = 10f;
    private void OnTriggerStay(Collider other)
    {
        HealthMA H = other.GetComponent<HealthMA>();
        if (H==null) return;
        {
            H.HealthPoints -= DamagePoints* Time.deltaTime;
        }
    }
}
