using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public int BaseDamage = 12;
    private bool CanDamage = true;
    void Start()
    {
        
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CanDamage)
        {
            Vector3 emitterPoint = other.GetComponentInChildren<DamagePos>().transform.position;
            GameObject.FindWithTag("Damage").GetComponent<DamageEmitterUI>().DoDamage(BaseDamage, emitterPoint);
            CanDamage = false;
            StartCoroutine(DelayStrike());
        }
    }

    private IEnumerator DelayStrike()
    {
        yield return new WaitForSeconds(1.2f);
        CanDamage = true;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
