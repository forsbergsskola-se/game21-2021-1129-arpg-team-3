using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDoorRaycast2MA : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;


    private ButtonDoorController2MA raycasteOBJ;
    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0 ;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;
    private const string interactableTag = "DoorButton";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position,fwd,out hit,rayLength,mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    raycasteOBJ = hit.collider.gameObject.GetComponent<ButtonDoorController2MA>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = false;
                if (Input.GetKeyDown(openDoorKey))
                {
                    raycasteOBJ.PlayAnimation();
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
         crosshair.color= Color.white;
        }
        else
        {
           crosshair.color=Color.white;
            isCrosshairActive = false;
        }
    }
}
