using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorMA : MonoBehaviour
{
    public GameObject door;
    public float minDistance;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Vector3.Distance(door.transform.position, transform.position) <= minDistance)
        {
            OpenThisDoor();
        }
    }

    private void OpenThisDoor()
    {
        Destroy(door);
    }
}
