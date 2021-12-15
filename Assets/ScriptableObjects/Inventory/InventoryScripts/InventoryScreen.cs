using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using TMPro;

public class InventoryScreen : MonoBehaviour
{
    // public GameObject inventoryPrefab;
    // public InventoryObjects inventory;
    // public int X_START;
    // public int Y_START;
    // public int X_SPACE_BETWEEN_ITEM;
    // public int NUMBER_OF_COLUMN;
    // public int Y_SPACE_BETWEEN_ITEM;
  //  private Dictionary<InventorySlotS, GameObject> itemDisplayed = new Dictionary<InventorySlotS, GameObject>();

//     void Start()
//     {
//         CreateDisplay();
//     }
//     void Update()
//     {
//        UpdateDisplay();
//     }
//     public void CreateDisplay()
//     {
//         for (int i = 0; i < inventory.Container.Items.Count; i++)
//         {
//             var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
//             obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = 
//             obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
//             obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[i].amount.ToString("n0");
//             itemDisplayed.Add(inventory.Container.Items[i], obj);
//         }
//     }
//
//     public Vector3 GetPosition(int i)
//     {
//         return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
//     }
//     
//     private void UpdateDisplay()
//     {
//         for (int i = 0; i < inventory.Container.Items.Count; i++)
//         {
//             if (itemDisplayed.ContainsKey(inventory.Container.Items[i]))
//             {
//                 itemDisplayed[inventory.Container.Items[i]].GetComponentInChildren<TextMeshProUGUI>().text =
//                     inventory.Container.Items[i].amount.ToString("n0");
//             }
//             else
//             {
//                 var obj = Instantiate(inventory.Container.Items[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
//                 obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
//                 obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[i].amount.ToString("n0");
//                 itemDisplayed.Add(inventory.Container.Items[i], obj);
//             }
//         }
//     }
 }
