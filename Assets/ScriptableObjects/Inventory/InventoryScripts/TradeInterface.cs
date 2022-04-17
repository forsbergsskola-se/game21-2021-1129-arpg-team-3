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

    private TextMeshProUGUI playerGoldDisplay;
    
 //   public delegate void EndTradeDelegate();
 //   public static event EndTradeDelegate OnEndTrade;

    public delegate void MakeSaleDelegate(ItemObject obj);

    public static event MakeSaleDelegate OnMakeSale;

    public delegate void OpenInventoryDelegate();

    public static event OpenInventoryDelegate OnOpenInventory;

    public delegate void UpdatePlayerGoldDelegate(int amountGold);

    public static event UpdatePlayerGoldDelegate OnUpdateGold;

    
    public override void CreateSlots()
    {
        SetupButtons();
        
    //    GameObject.FindWithTag("Player").GetComponent<PlayerStatsLoader>().playerStats.Gold += 1000;

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

        OpenInventory();
        UpdatePlayerGoldDisplay((int)GameObject.FindWithTag("Player").GetComponent<PlayerStatsLoader>().playerStats.Gold);
    }
    

    private void UpdatePlayerGoldDisplay(int goldAmount)
    {
        if (OnUpdateGold != null)
        {
            OnUpdateGold(goldAmount);
        }
    }
    
    private void OpenInventory()
    {
        
        if (OnOpenInventory != null)
        {
            OnOpenInventory();
        }

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

    private void MakeSale(ItemObject obj)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerStatsLoader>().playerStats.Gold -= obj.baseValue + discountMarkup;
        UpdatePlayerGoldDisplay((int)GameObject.FindWithTag("Player").GetComponent<PlayerStatsLoader>().playerStats.Gold);
        
        if (OnMakeSale != null)
        {
            OnMakeSale(obj);
        }
    }
    
    private void OnPointerClick(GameObject obj, int slotIndex)
    {
        inventory.GetSlots
            .Where(x => x.slotDisplay.transform.position != obj.transform.position).ToList()
            .Select(x => x.slotDisplay.transform.GetComponentsInChildren<Image>()[0].color = new Color(0.5f, 0.5f, 0.5f, 1)).ToList();
        obj.transform.GetComponentsInChildren<Image>()[0].color = new Color(1, 1, 1, 1);
        
        int totalCost = itemToAdd[slotIndex].baseValue + discountMarkup;
        GameObject.FindWithTag("BuyButton").GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Cost: "  + totalCost;
        var buyButton = GameObject.FindWithTag("BuyButton").GetComponentInChildren<Button>();
        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = true;
        buyButton.onClick.AddListener(() => BuyButtonClick(obj, slotIndex));
    }

    private void BuyButtonClick(GameObject obj, int index)
    {
        float gold = GameObject.FindWithTag("Player").GetComponent<PlayerStatsLoader>().playerStats.Gold;

        if (items[index].amount > 0 && gold >= itemToAdd[index].baseValue + discountMarkup)
        {
            items[index].amount--;
            obj.transform.GetComponentInChildren<TextMeshProUGUI>().text = items[index].amount.ToString();
            MakeSale(itemToAdd[index]);
        }
    }
    
    
    
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)),
            Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
