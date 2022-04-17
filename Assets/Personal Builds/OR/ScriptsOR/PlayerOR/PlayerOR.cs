using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOR : MonoBehaviour//, IDamageableOR
{
    [SerializeField] private AttackRadiusOR _attackRadius;
    [SerializeField] private Animator Animator;
    private Coroutine _lookCoroutine;
   // [SerializeField] private int health = 300;
    private const string AttackTriggerOR = "Attack";

    private void Awake()
    {
        _attackRadius.OnAttack += OnAttack;
    }

    private void OnAttack(IDamageableOR target)
    {
        Animator.SetTrigger(AttackTriggerOR);

        if (_lookCoroutine != null)
        {
            StopCoroutine(_lookCoroutine);
        }
    }
    
}
