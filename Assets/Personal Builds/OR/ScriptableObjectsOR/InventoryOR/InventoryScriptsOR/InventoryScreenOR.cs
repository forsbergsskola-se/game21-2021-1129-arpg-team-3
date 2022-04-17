using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryScreenOR : MonoBehaviour
{
    public InventoryObjectOR inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM_OR;
    public int NUMBER_OF_COLUMN_OR;
    public int Y_SPACE_BETWEEN_ITEM_OR;
    private Dictionary<InventorySlotOR, GameObject> itemDisplayed = new Dictionary<InventorySlotOR, GameObject>();

    void Start()
    {
        CreateDisplay();
    }
    void Update()
    {
       UpdateDisplay();
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.ContainerOR.Count; i++)
        {
            var obj = Instantiate(inventory.ContainerOR[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.ContainerOR[i].amount.ToString("n0");
            itemDisplayed.Add(inventory.ContainerOR[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM_OR *(i % NUMBER_OF_COLUMN_OR)), Y_START + (-Y_SPACE_BETWEEN_ITEM_OR * (i / NUMBER_OF_COLUMN_OR)), 0f);
    }
    
    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.ContainerOR.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.ContainerOR[i]))
            {
                itemDisplayed[inventory.ContainerOR[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.ContainerOR[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.ContainerOR[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.ContainerOR[i].amount.ToString("n0");
                itemDisplayed.Add(inventory.ContainerOR[i], obj);
            }
        }
    }
}
