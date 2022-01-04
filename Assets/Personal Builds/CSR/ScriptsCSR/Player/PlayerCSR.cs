// Cecilija Simic Rihtnesberg
//     2021-11-29

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCSR : MonoBehaviour
{
    
    
    private NavMeshAgent _agent;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigitbodyComponenet;
    private int superJumpsRemaining;
    
    public int health = 3;
    
  
    private void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();

    }


    void collidedWithEnemy(EnemyCSR enemy) {
        enemy.Attack(this);
        if(health <= 0) {
            // Todo 
        }
    }

    void OnCollisionEnter (Collision col) {
        EnemyCSR enemy = col.collider.gameObject.GetComponent<EnemyCSR>();
        if(enemy) {
            collidedWithEnemy(enemy);
        }

    }

    private void Move(Vector3 point)
    {
        _agent.SetDestination(point);
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                print($"Hit {hitInfo.collider.name} ");
                Move(hitInfo.point);
            }

        }
            
    }
    
    
    private void FixedUpdate()
    {
       

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigitbodyComponenet.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    
}
