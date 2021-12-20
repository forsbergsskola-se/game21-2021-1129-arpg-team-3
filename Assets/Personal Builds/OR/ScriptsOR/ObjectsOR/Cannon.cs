using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public float shootForce = 0f;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(cannonBall, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
        }
    }
}
