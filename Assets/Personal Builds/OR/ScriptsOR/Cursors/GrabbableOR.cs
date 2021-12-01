using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableOR : MonoBehaviour
{
    private bool canGrab;
    void Update()
    {
        if (canGrab)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CursorOR.instance.ActivateHandClosed();
            }

            if (Input.GetMouseButtonUp(0))
            {
                CursorOR.instance.ActivateHandOpen();
            }
        }
    }

    private void OnMouseEnter()
    {
        CursorOR.instance.ActivateHandOpen();
        canGrab = true;
    }
    private void OnMouseExit()
    {
        CursorOR.instance.ClearCursor();
        canGrab = false;
    }
    
    
}
