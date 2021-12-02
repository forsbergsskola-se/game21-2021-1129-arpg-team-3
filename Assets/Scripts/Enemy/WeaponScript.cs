// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class WeaponScript : MonoBehaviour
// {
//     [SerializeField] int BaseDamage = 12;
//     [SerializeField] float StrikeDelay = 1.6f;
//     [SerializeField] string targetTag = "Player";
//     private bool CanDamage = true;
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag(targetTag) && CanDamage)
//         {
//             Vector3 emitterPoint = other.GetComponentInChildren<DamagePos>().transform.position;
//             GameObject.FindWithTag("Damage").GetComponent<DamageEmitterUI>().DoDamage(BaseDamage, emitterPoint);
//             CanDamage = false;
//             StartCoroutine(DelayStrike());
//         }
//     }
//
//     private IEnumerator DelayStrike()
//     {
//         yield return new WaitForSeconds(StrikeDelay);
//         CanDamage = true;
//
//     }
//     
//     // Update is called once per frame
//     void Update()
//     {
//         
//     }
// }
