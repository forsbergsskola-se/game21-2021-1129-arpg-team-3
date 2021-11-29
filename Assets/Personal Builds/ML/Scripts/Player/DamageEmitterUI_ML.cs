using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum DamageState
{
    Ready,
    Playing,
    Finished
}

[Serializable]
public struct DamageStruct
{
    public Canvas canvas;
  //  public TextMeshPro mesh;
    public float colorAlpha;
    public DamageState state;
}



public class DamageEmitterUI_ML : MonoBehaviour
{
    public Canvas damageCanvas;
    public List<DamageStruct> damageList = new List<DamageStruct>();
    private TextMeshPro mesh;
    private Color32 redColor = new Color32(255, 0,0, 255);
    private bool moveText = false;
    
    
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
