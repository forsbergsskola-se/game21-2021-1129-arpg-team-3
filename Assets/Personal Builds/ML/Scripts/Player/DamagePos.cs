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
    }

    private void ShrinkBar()
    {
        HealthBar.GetComponentInChildren<Canvas>().GetComponentInChildren<Canvas>();
    }
    
    private void LateUpdate()
    {
        HealthBar.transform.position = gameObject.transform.position + new Vector3(0,0.5f, 0);
    }
}
