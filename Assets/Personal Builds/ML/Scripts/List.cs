using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class List : MonoBehaviour
{
    [SerializeField] int itemCount = 5;
    [SerializeField] ListItem itemPrefab;
    [SerializeField] RectTransform itemContainer;
    [SerializeField] private GameObject testImage;
    void Start () 
    {
        for (int i = 0; i < itemCount; i++)
        {
            var label = string.Format($"Item {i}");

            CreateNewListItem(label);
        }
    }

    public void CreateNewListItem(string label)
    {
        var newItem = Instantiate(testImage);
        newItem.transform.SetParent(itemContainer, worldPositionStays: false);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(10);

    }
}
