using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovementYY : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private Vector3 previousPosition;
    public float LookUp = 60;
    public float LookDown = -60;

    
    

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;
            cam.transform.Rotate(new Vector3(0,0,0),direction.y*90); // Controls up and down 2 to 85 deg
            cam.transform.Rotate(new Vector3(0,-0.1f,0),-direction.x*90,Space.World);
            cam.transform.Translate(new Vector3(0,0,-10));
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}