using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour 
{
    public PlayerStats playerStats;
    public GameObject cannonBall;
    public GameObject sword;
    public float shootForce;
    private bool canAttack = true;
    private bool canSwing = true;

    private void Start() {
        playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && playerStats.secondary && canAttack)
        {
            GameObject projectile = Instantiate(cannonBall, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
            StartCoroutine(DelayAttack());
        }
        else if (Input.GetMouseButtonDown(0) && canSwing) {
            GameObject projectile = Instantiate(sword, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * (shootForce - 500f));
            StartCoroutine(DelaySwing());
        }
    }
    
    private IEnumerator DelayAttack() {
        canAttack = false;
        yield return new WaitForSeconds(2.5f);
        canAttack = true;
    }
    private IEnumerator DelaySwing() {
        canSwing = false;
        yield return new WaitForSeconds(0.5f);
        canSwing = true;
    }
}
