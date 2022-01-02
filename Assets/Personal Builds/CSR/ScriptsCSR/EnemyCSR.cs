using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCSR : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public int damage;
    public Transform targetTransform;
    
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

    
    void FixedUpdate () {
        if(targetTransform != null) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetTransform.transform.position, Time.deltaTime * moveSpeed);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void Attack(PlayerCSR player) {
        player.health -= this.damage;
        Destroy(this.gameObject);
    }

}
