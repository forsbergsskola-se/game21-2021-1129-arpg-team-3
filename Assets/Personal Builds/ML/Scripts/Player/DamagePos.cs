using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePos : MonoBehaviour
{
    public Canvas HealtBarToUse;
    private Canvas HealthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        HealthBar = Instantiate(HealtBarToUse);
        HealthBar.renderMode = RenderMode.WorldSpace;
        
        HealthBar.GetComponentInChildren<Canvas>().transform.position = gameObject.transform.position + new Vector3(0,0.5f, 0);
    //    ShrinkBar();
    }

    private void ShrinkBar()
    {
        HealthBar.GetComponentInChildren<Canvas>().GetComponentInChildren<Canvas>().transform.localScale = new Vector3(1f,1f);
    }
    
    private void LateUpdate()
    {
        HealthBar.transform.position = gameObject.transform.position + new Vector3(0,0.5f, 0);
    }
}
