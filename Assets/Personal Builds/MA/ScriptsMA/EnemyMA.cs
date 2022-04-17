using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMA : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
        }
    }

    
}
