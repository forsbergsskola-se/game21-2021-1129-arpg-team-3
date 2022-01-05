using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class ItemHolder
{
    public int amount = 7;
    public Sprite displayImage;
}

public class TradeInterface : UserInterface
{
    [SerializeField] private int discountMarkup;
    public GameObject inventoryPrefab;
    public List <ItemObject> itemToAdd;
    public List<ItemHolder> items;
    public GameObject itemInfoDisplay;
    private readonly int X_START = -100;
    private readonly int Y_START =  186;
    private readonly int X_SPACE_BETWEEN_ITEM = 65;
    private readonly int NUMBER_OF_COLUMN = 4;
    private readonly int Y_SPACE_BETWEEN_ITEM = 50;
    
    public override void CreateSlots()
    {
        SetupButtons();

        slotsOnInterface = new Dictionary<GameObject, InventorySlotS>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerEnter,delegate{OnEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit,delegate{OnExit(obj);});
            AddEvent(obj, EventTriggerType.PointerClick, delegate{OnPointerClick(obj);});
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }

        DoAThing(0);
    }

    private void GetLowestIndex()
    {
        
    }
    
    private void DoAThing(int index)
    {
        bool lowest = false;
        inventory.GetSlots[index].slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = itemToAdd[index].uiDisplay;
        inventory.GetSlots[index].slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = items[index].amount.ToString();
        inventory.GetSlots[index].slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
    }
    
    private void SetupButtons()
    {
        GameObject.FindWithTag("BuyButton").GetComponent<Button>().interactable = false;
    }

    private void OnPointerClick(GameObject obj)
    {
        obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0, 1, 0, 1);
        var buyButton = GameObject.FindWithTag("BuyButton").GetComponent<Button>();
        buyButton.interactable = true;
        buyButton.onClick.AddListener(() => BuyButtonClick(obj));
    }

    private void BuyButtonClick(GameObject obj)
    {
        inventory.AddItem(itemToAdd[0].data, 1);

        foreach (var el in inventory.GetSlots)
        {
            Debug.Log(el.amount);
        }
    }
    
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)),
            Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
