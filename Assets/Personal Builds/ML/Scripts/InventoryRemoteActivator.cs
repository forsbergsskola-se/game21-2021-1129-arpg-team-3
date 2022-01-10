using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryRemoteActivator : MonoBehaviour
{
    private Canvas canvas;
    
    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        TradeInterface.OnOpenInventory += OpenCloseRemote;
    }
    
    private void OpenCloseRemote()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}