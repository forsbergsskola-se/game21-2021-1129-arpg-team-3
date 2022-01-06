using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] Image buySellPanel;
    public List <ItemObject> itemToAdd;
    public List<ItemHolder> items;
    public GameObject itemInfoDisplay;
    private readonly int X_START = -100;
    private readonly int Y_START =  186;
    private readonly int X_SPACE_BETWEEN_ITEM = 65;
    private readonly int NUMBER_OF_COLUMN = 4;
    private readonly int Y_SPACE_BETWEEN_ITEM = 50;

    public delegate void MakeSaleDelegate(GameObject obj, int slotIndex);

    public static event MakeSaleDelegate OnMakeSale;
    
    public override void CreateSlots()
    {
        SetupButtons();

        slotsOnInterface = new Dictionary<GameObject, InventorySlotS>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            int index = i;
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerEnter,delegate{OnEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit,delegate{OnExit(obj);});
            AddEvent(obj, EventTriggerType.PointerClick, delegate{OnPointerClick(obj, index);});
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }

        SetupSlots();
    }

    private void SetupSlots()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                itemToAdd[i].uiDisplay;
            inventory.GetSlots[i].slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text =
                items[i].amount.ToString();
            inventory.GetSlots[i].slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color =
                new Color(1, 1, 1, 1);
        }
    }
    
    private void SetupButtons()
    {
        GameObject.FindWithTag("BuyButton").GetComponentInChildren<Button>().interactable = false;
    }

    private void TryMakeSale(GameObject obj, int slotIndex)
    {
        if (OnMakeSale != null)
        {
            OnMakeSale(obj, slotIndex);
        }
    }
    
    private void OnPointerClick(GameObject obj, int slotIndex)
    {
        inventory.GetSlots
            .Where(x => x.slotDisplay.transform.position != obj.transform.position).ToList()
            .Select(x => x.slotDisplay.transform.GetComponentsInChildren<Image>()[0].color = new Color(1, 1, 1, 1)).ToList();

        obj.transform.GetComponentsInChildren<Image>()[0].color = new Color(0, 1, 0, 1);

        GameObject.FindWithTag("BuyButton").GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Cost: "  + itemToAdd[slotIndex].baseValue;
        var buyButton = GameObject.FindWithTag("BuyButton").GetComponentInChildren<Button>();
        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = true;
        buyButton.onClick.AddListener(() => BuyButtonClick(obj, slotIndex));
        
        TryMakeSale(obj, slotIndex);
    }

    private void BuyButtonClick(GameObject obj, int index)
    {
        if (items[index].amount > 0)
        {
            items[index].amount--;
            obj.transform.GetComponentInChildren<TextMeshProUGUI>().text = items[index].amount.ToString();
        }
    }
    
    
    
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)),
            Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
