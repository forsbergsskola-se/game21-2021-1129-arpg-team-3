using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusOR : MonoBehaviour
{
    private List<IDamageableOR> _damageablesOR = new List<IDamageableOR>();
    public int damage = 10;
    public float attackDelay = 0.5f;
    public delegate void AttackEventOR(IDamageableOR Target);
    public AttackEventOR OnAttack;
    private Coroutine _attackCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        IDamageableOR damageableOR = other.GetComponent<IDamageableOR>();
        if (damageableOR != null)
        {
            _damageablesOR.Add(damageableOR);
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(AttackOR());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageableOR damageableOR = other.GetComponent<IDamageableOR>();
        if (damageableOR != null)
        {
            _damageablesOR.Remove(damageableOR);
            if (_damageablesOR.Count == 0)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackOR()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        yield return wait;
        IDamageableOR closestDamageable = null;
        float closestDistance = float.MaxValue;

        while (_damageablesOR.Count > 0)
        {
            for (int i = 0; i < _damageablesOR.Count; i++)
            {
                Transform damageableTransform = _damageablesOR[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = _damageablesOR[i];
                }
            }
            if (closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(damage);
            }
            closestDamageable = null;
            closestDistance = float.MaxValue;
            yield return wait;

            _damageablesOR.RemoveAll(DisabledDamageables);
        }
        _attackCoroutine = null;
    }

    private bool DisabledDamageables(IDamageableOR damageableOR)
    {
        return damageableOR != null && !damageableOR.GetTransform().gameObject.activeSelf;
    }
}
