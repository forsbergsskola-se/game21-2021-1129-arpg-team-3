using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;
   
    void Start()
    {
        StoreList.OnMadeSale += IndexSale;
    }

    private void IndexSale(InventoryItemObject obj)
    {
        inventory.AddItem(obj, 1);
    }

}
