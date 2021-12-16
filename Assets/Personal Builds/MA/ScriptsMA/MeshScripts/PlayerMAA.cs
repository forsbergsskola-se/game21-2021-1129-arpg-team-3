using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMAA : MonoBehaviour, IDamageableMA
{
    [SerializeField] private AttackRadius attackRadius;
    private Coroutine lookCoroutine;
    [SerializeField] private int health = 300;
    
    private Animator animator;
    
    
    private const string ATTACK_TRIGGER = "Attack";
    private void Awake()
    {
       //attackRadius.onAttack += OnAttack;
    }

    private void OnAttack(IDamageableMA Target)
    {

        if (lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }

        lookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
    }
    

    private IEnumerator LookAt(Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * 2;
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <=0)
        {
            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
