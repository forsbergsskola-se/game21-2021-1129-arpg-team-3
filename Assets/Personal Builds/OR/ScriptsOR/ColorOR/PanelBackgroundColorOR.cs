using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PanelBackgroundColorOR : MonoBehaviour
{
    private Image panel;
    public ColorDataOR color;

    // private void AdjustColor(ColorReference _color)
    // {
    //     if (panel == null)
    //         panel = this.GetComponent<Image>();
    //
    //     panel.color = _color.Value;
    // }
}