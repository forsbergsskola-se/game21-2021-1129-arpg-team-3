using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject Prefab;
    public float virusForce = 20f;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
      GameObject virus =  Instantiate(Prefab, firePoint.position, firePoint.rotation);
     Rigidbody rb =  virus.GetComponent<Rigidbody>();
     rb.AddForce(firePoint.up*virusForce,ForceMode.Impulse);
    }
}
