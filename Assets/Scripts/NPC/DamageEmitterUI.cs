using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageEmitterUI : MonoBehaviour
{
    public Canvas damageCanvas;
  
    private TextMeshPro mesh;
  
    public void DoDamage(int damageAmount, Vector3 position)
    {
        Canvas tempCan = Instantiate(damageCanvas);
        tempCan.GetComponent<TextMeshPro>().transform.position = position;
        tempCan.GetComponent<TextMeshPro>().fontSize = 6;
        tempCan.GetComponent<TextMeshPro>().autoSizeTextContainer = true;
        tempCan.GetComponent<TextMeshPro>().text = "-" + Convert.ToString(damageAmount);
        tempCan.GetComponent<TextMeshPro>().color = new Color32(255, 0, 0, 255);
    }

}
