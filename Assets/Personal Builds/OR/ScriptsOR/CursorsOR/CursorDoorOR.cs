using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDoorOR : MonoBehaviour
{
    private bool canOpen;
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        // if (canOpen == true)
        // {
        //     CursorOR.instance.DoorUnlocked();
        // }
        //
        // if (canOpen == false)
        // {
        //     CursorOR.instance.DoorLocked();
        // }
        CursorOR.instance.DoorUnlocked();
    }
    private void OnMouseExit()
    {
        CursorOR.instance.ClearCursor();
        
    }

}
