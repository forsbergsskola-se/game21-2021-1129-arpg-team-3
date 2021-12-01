using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   public float cubeSize = 0.2f;
   public int cubesInRow = 5;
   private float cubesPivotDistance;
   private Vector3 cubesPivot;
   public float explosionRadius;
   public float explosionUpward;
   public float explosionForce;

   private void Start()
   {
       cubesPivotDistance = cubeSize * cubesInRow / 2;

       cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

   }

   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Ground")
        {
            explode();
        }
    }

    public void explode()
    {
        gameObject.SetActive(false);

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x,y,z);
                }
            }
        }

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb!=null)
            {
                rb.AddExplosionForce(explosionForce,transform.position,explosionRadius,explosionUpward);
            }
        }
    }

    void createPiece(int x,int y,int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position + new Vector3(cubeSize*x,cubeSize*y,cubeSize*z)-cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize,cubeSize,cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
}
