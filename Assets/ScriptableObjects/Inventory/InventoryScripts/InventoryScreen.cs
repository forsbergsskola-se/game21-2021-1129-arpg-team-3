using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObjects inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;
    Dictionary<GameObject, InventorySlotS> itemsDisplayed = new Dictionary<GameObject, InventorySlotS>();

    void Start()
    {
        CreateSlots();
    }
    void Update()
    {
    }
   
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlotS>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
