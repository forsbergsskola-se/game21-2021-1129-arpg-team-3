using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyYY 
{
    public EnemyMovementYY Movement;
    public NavMeshAgent Agent;
    public EnemyScriptableObjectYY EnemyScriptableObject;
    public int Health = 100;

    public virtual void OnEnable()
    {
        
    }

    //public override void OnDisable()
    //{
        
    //}
}
