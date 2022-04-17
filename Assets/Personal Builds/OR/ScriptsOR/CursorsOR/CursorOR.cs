using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CursorOR : MonoBehaviour
{
    public static CursorOR instance;
    [FormerlySerializedAs("dRPGCursor")] public Texture2D arrowCursor;
    public Texture2D crosshairs, handOpen, handClosed, doorUnlocked, doorLocked;

    public void Awake()
    {
        instance = this;
    }
    
    public void ActivateRPGCursor()
    {
        Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.Auto);
    }

    public void ActivateCrosshair()
    {
        Cursor.SetCursor(crosshairs, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
    }

    public void ActivateHandOpen()
    {
        Cursor.SetCursor(handOpen, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
    }
    public void ActivateHandClosed()
    {
        Cursor.SetCursor(handClosed, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
    }

    public void ClearCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
     public void DoorUnlocked()
       {
           Cursor.SetCursor(doorUnlocked, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
       }
      public void DoorLocked()
        {
            Cursor.SetCursor(doorLocked, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
        }
}
