using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    [SerializeField] InventoryObject_ML inventory;
    public static int amountCash = 60;

    public delegate void TryMakePurchaseDelegate(int amountMoney);

    public static event TryMakePurchaseDelegate OnTryMakePurchase;


    private void TryPurchase(int amountMoney)
    {
        if (OnTryMakePurchase != null)
        {
            OnTryMakePurchase(amountMoney);
        }
    }
   
    void Start()
    {
        StoreList.OnMadeSale += IndexSale;
    }

    private void IndexSale(InventoryItemObject_ML obj, int cost)
    {
        inventory.AddItem(obj, 1);
        amountCash -= cost;
        Debug.Log(inventory.container[0].amount);
        Debug.Log($"Amount money: {amountCash}");
    }

}
