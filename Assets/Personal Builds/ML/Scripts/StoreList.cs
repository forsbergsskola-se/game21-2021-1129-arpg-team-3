using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreList : MonoBehaviour
{
    [SerializeField] RectTransform itemContainer;
    [SerializeField] private GameObject testImage;
    [SerializeField] private PlayerInventoryContainerTest container;
    void Start ()
    {
        InventoryItem item = new InventoryItem
        {
            itemType = ItemType.Potion,
            Value = 12
        };
        container = ScriptableObject.CreateInstance<PlayerInventoryContainerTest>();
        container.items.Add(item);
    }

    

    public void AddItem()
    {
        var newItem = Instantiate(testImage, itemContainer, false);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(10);
    }
    
    public void CreateNewListItem(string label)
    {
        var newItem = Instantiate(testImage, itemContainer, false);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(10);

    }
}
