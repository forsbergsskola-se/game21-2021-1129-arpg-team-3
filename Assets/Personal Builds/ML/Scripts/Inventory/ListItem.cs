using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI labelText;

    public string Label
    {
        get
        {
            return labelText.text;
        }
        set
        {
            labelText.text = value;
        }
    }
}
