using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public float shootForce;
    private bool canAttack = true;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
            GameObject projectile = Instantiate(cannonBall, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
            StartCoroutine(DelayAttack());
        }
    }
    
    private IEnumerator DelayAttack() {
        canAttack = false;
        yield return new WaitForSeconds(2.5f);
        canAttack = true;
    }
}
