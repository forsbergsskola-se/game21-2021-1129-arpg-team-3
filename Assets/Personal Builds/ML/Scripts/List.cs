using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class List : MonoBehaviour
{
    [SerializeField] int itemCount = 5;
    [SerializeField] RectTransform itemContainer;
    [SerializeField] private GameObject testImage;
    void Start () 
    {
        
    }


    public void AddItem()
    {
        var newItem = Instantiate(testImage);
        newItem.transform.SetParent(itemContainer, false);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(10);
    }
    
    public void CreateNewListItem(string label)
    {
        var newItem = Instantiate(testImage);
        newItem.transform.SetParent(itemContainer, worldPositionStays: false);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(10);

    }
}
