using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementMA : MonoBehaviour
{
    public Transform target;
    public float Height = 10;
    public float radius = 10;
    public float angle = 0;
    public float rotationalSpeed = 36f;

    
    void Update()
    {
        float cameraX = target.position.x + (radius * Mathf.Cos(Mathf.Deg2Rad * angle));
        float cameraY = target.position.y + Height;
        float cameraZ = target.position.z + (radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        transform.position = new Vector3(cameraX, cameraY, cameraZ);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            angle = angle - rotationalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            angle = angle + rotationalSpeed * Time.deltaTime;
        }
        transform.LookAt(target.position);
    }
}
