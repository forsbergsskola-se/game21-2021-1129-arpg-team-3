using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorMA : MonoBehaviour
{
    public GameObject Door;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenThisDoor();
        }
    }

    private void OpenThisDoor()
    {
        Destroy(Door);
    }
}
