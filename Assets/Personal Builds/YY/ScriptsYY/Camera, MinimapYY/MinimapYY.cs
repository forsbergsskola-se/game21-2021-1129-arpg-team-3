using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapYY : MonoBehaviour
{
    public Transform miniMapCameraTransform;
    public Transform playerTransform;
    private Vector3 cameraFromPlayerOffset;
    [SerializeField]float rotationSpeed = 120f;
    [SerializeField]Transform player;
    
    Vector3 offset;
    private Vector3 previousPosition;
    Vector3 _velocity = Vector3.zero;
    
    void Start()
    {
        cameraFromPlayerOffset = new Vector3(0, 11, 0);
        offset = transform.position - player.position;
    }
    private void LateUpdate() 
    {
        miniMapCameraTransform.position = playerTransform.position + cameraFromPlayerOffset;
        var position = player.position;
        var targetPosition = position + offset;
        transform.RotateAround(position, player.up, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed);
    }
}