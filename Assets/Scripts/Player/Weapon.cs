using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    private PlayerStats playerStats;
    public GameObject cannonBall;
    // public GameObject sword;
    public GameObject fire1;
    public GameObject fire2;
    public float shootForce;
    private bool canAttack = true;
    // private bool canSwing = true;
    private bool canImmolate = true;

    private void Start() 
    {
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
        // else if (Input.GetMouseButtonDown(0) && canSwing)
        // {
        //     GameObject projectile = Instantiate(sword, transform.position, transform.rotation);
        //     projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * (shootForce - 500f));
        //     StartCoroutine(DelaySwing());
        // }
        else if (Input.GetKeyDown(KeyCode.Space) && playerStats.tertiary && canImmolate)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/FireCircle");
            fire1.GetComponent<ParticleSystem>().Play();
            fire2.GetComponent<ParticleSystem>().Play();
            StartCoroutine(Immolate());
            StartCoroutine(DelayImmolate());
        }
    }
    
    private IEnumerator DelayAttack() 
    {
        canAttack = false;
        yield return new WaitForSeconds(2.5f);
        canAttack = true;
    }
    // private IEnumerator DelaySwing() {
    //     canSwing = false;
    //     yield return new WaitForSeconds(1f);
    //     canSwing = true;
    // }
    private IEnumerator Immolate() 
    {
        fire1.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(4f);
        fire1.GetComponent<Collider>().enabled = false;
    }
    private IEnumerator DelayImmolate() 
    {
        canImmolate = false;
        yield return new WaitForSeconds(10f);
        canImmolate = true;
    }
}
