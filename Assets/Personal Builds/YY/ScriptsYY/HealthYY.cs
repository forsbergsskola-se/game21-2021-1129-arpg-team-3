using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthYY : MonoBehaviour
{
    public float startingHealth = 100f;
    public GameObject damageText;

    public float HealthPoints
    {
        get { return _HealthPoints; }
        set
        {
            _HealthPoints = Mathf.Clamp(value,0f,100f);

            if (_HealthPoints<=0f)
            {
                Die();
            }
        }
    }
    
    [SerializeField]
    private float _HealthPoints = 100f;


    private void Start()
    {
        HealthPoints = startingHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}