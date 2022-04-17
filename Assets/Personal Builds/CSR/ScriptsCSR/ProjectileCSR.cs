using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCSR : MonoBehaviour
{
    public float speed;
    public int damage;

    Vector3 shootDirection;

    // 1
    void FixedUpdate () {
        this.transform.Translate(shootDirection * speed, Space.World);
    }

// 2
    public void FireProjectile(Ray shootRay) {
        this.shootDirection = shootRay.direction;
        this.transform.position = shootRay.origin;
    }

// 3
    void OnCollisionEnter (Collision col) {
        EnemyCSR enemy = col.collider.gameObject.GetComponent<EnemyCSR>();
        if(enemy) {
            enemy.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

}
