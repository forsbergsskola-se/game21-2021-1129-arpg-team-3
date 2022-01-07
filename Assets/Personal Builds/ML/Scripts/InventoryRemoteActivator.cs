using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class InventoryRemoteActivator : MonoBehaviour
{
    private Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        TradeInterface.OnOpenInventory += OpenCloseRemote;
    }
    
    private void OpenCloseRemote()
    {
        canvas.GetComponentsInChildren<Image>()[1].enabled = false;
        canvas.GetComponentsInChildren<Image>()[1].GetComponentsInChildren<Image>().Select(x => x.enabled = false).ToList();
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            canvas.GetComponentsInChildren<Image>()[1].enabled = false;
            canvas.GetComponentsInChildren<Image>()[1].GetComponentsInChildren<Image>().Select(x => x.enabled = false).ToList();
        }
        else
        {
        //    canvas.GetComponentsInChildren<Image>()[1].enabled = true;
        //    canvas.GetComponentsInChildren<Image>()[1].GetComponentsInChildren<Image>().Select(x => x.enabled = true).ToList();
            //canvas.enabled = false;
        }
    }
}
