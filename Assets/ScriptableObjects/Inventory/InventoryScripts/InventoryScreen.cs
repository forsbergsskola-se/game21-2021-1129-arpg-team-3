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
    Dictionary<InventorySlotS, GameObject> itemDisplayed = new Dictionary<InventorySlotS, GameObject>();

    void Start()
    {
        CreateDisplay();
    }
    void Update()
    {
       UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlotS slot = inventory.Container.Items[i];
            if (itemDisplayed.ContainsKey(slot))
            {
                itemDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text =
                    slot.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemDisplayed.Add(slot, obj);
            }
        }
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlotS slot = inventory.Container.Items[i];
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                inventory.database.GetItem[slot.item.Id].uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            itemDisplayed.Add(slot, obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
