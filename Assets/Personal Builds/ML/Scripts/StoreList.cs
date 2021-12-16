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
    public InventoryObject_ML inventory;

    public delegate void MadeSaleDelegate(InventoryItemObject_ML obj, int cost);
    public static event MadeSaleDelegate OnMadeSale;


    private void MakeSale(InventoryItemObject_ML obj, int cost)
    {
        if (OnMadeSale != null)
        {
            OnMadeSale(obj, cost);
        }
    }

    private void TryMakeSale(InventoryItemObject_ML obj, int cost)
    {
        if (cost <= TestInventory.amountCash)
        {
            MakeSale(obj, cost);
        }
        else
        {
            Debug.Log("You can't afford to buy it!");
        }
    }
    
    private void SetupButton(int index, Button button)
    {
        InventoryItemObject_ML buyObj = inventory.container[index].item;
        int itemCost = inventory.container[index].item.baseValue;
        button.onClick.AddListener(()=> TryMakeSale(buyObj, itemCost));
    }
    

    public void AddItems()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var newItem = Instantiate(testImage, itemContainer, false);
            var button = newItem.GetComponentInChildren<Button>();
            newItem.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Cost: " + inventory.container[i].item.baseValue;
            newItem.GetComponentsInChildren<TextMeshProUGUI>()[2].text =  inventory.container[i].item.description;
            newItem.GetComponentsInChildren<Image>()[1].sprite = inventory.container[i].item.displayImage;
            SetupButton(i, button);
        }
    }
}
