// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
//
// public class DamageEmitter : MonoBehaviour
// {
//     private float colorAlpha = 0;
//     private Color32 colors = new Color32(255, 0,0, 255);
//     
//     
//     void Update()
//     {
//         gameObject.GetComponent<TextMeshPro>().transform.position += Vector3.up * 0.2f * Time.deltaTime;
//         gameObject.GetComponent<TextMeshPro>().color = Color32.Lerp(colors, new Color32(255, 0, 0, 0), colorAlpha);
//         colorAlpha += Time.deltaTime / 5f;
//         StartCoroutine(DelayDestroy());
//     }
//
//     private IEnumerator DelayDestroy()
//     {
//         yield return new WaitUntil(() => colorAlpha > 1);
//         Destroy(gameObject);
//     }
//     
// }
