using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCursorOR : MonoBehaviour
{
    private void OnMouseEnter()
    {
        CursorOR.instance.ActivateRPGCursor();
    }

    private void OnMouseExit()
    {
        
    }
}
