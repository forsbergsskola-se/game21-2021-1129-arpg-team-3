using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreList : MonoBehaviour
{
    [SerializeField] RectTransform itemContainer;
    [SerializeField] private GameObject testImage;
    [SerializeField] private PlayerInventoryContainerTest container;
    public InventoryObject inventory;

    public delegate void MadeSaleDelegate();
    public static event MadeSaleDelegate OnMadeSale;


    private void MakeSale()
    {
        if (OnMadeSale != null)
        {
            OnMadeSale();
        }
    }
    
    void Start ()
    {
        
    }

    
    

    private void SetupButton(int index, Button button)
    {
        button.onClick.AddListener(()=> MakeSale());
    }
    

    public void AddItem()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var newItem = Instantiate(testImage, itemContainer, false);
            var button = newItem.GetComponentInChildren<Button>();
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = "Cost: " + inventory.container[i].item.baseValue;
            newItem.GetComponentsInChildren<Image>()[1].sprite = inventory.container[i].item.displayImage;
            SetupButton(i, button);
        }
    }
    
    public void CreateNewListItem(string label)
    {
        var newItem = Instantiate(testImage, itemContainer, false);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(10);

    }
}
