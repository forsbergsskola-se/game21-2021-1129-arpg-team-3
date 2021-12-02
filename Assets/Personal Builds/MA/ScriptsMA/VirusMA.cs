using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMA : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
       {
           if (collision.transform.tag=="Enemy")
           {
               Destroy(collision.gameObject);
           }
       }
   }

