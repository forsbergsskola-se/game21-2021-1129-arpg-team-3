using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ScriptObjectMA : ScriptableObject
{
    [SerializeField] private float health;

    [SerializeField] private float maxHealth;

    [SerializeField] private float armor;



    public float Health
    {
        get => health;
        set
        {
            health = value;
            health = Mathf.Clamp(health, 0f, maxHealth);
        }
    }

    public float MaxHealth => maxHealth;


    public float Armor => armor;
    
}
