using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCursorOR1 : MonoBehaviour
{
    private void OnMouseEnter()
    {
        CursorOR.instance.ActivateCrosshair();
    }

    private void OnMouseExit()
    {
        CursorOR.instance.ActivateRPGCursor();
    }
}
