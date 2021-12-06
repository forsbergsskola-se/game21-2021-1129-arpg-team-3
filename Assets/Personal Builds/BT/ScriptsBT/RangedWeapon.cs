// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class RangedWeapon : MonoBehaviour
// {
//     [SerializeField] new ParticleSystem particleSystem;
//     
//     private float _timeRemaing;
//     
//
//     private void Awake()
//     {
//         particleSystem = GetComponent<ParticleSystem>();
//         particleSystem.Stop();
//     }
//
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space)) StartFiring();
//         if (Input.GetKeyUp(KeyCode.Space)) StopFiring();
//     }
//
//     private void StartFiring()
//     {
//         particleSystem.Play();
//     }
//
//     private void StopFiring()
//     {
//         particleSystem.Stop();
//     }
// }
