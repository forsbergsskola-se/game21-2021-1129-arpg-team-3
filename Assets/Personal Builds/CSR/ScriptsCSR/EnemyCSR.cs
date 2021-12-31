using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCSR : MonoBehaviour
{
    public int health = 3;

    void collidWithEnemy(Enemy enemy)
    {
        if (health >= 0)
        {
            
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
        collidWithEnemy(enemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
