using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
    public InventoryObjects inventory;
    public Dictionary<GameObject, InventorySlotS> slotsOnInterface = new Dictionary<GameObject, InventorySlotS>();
    void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter,delegate{OnEnterInterface(gameObject);});
        AddEvent(gameObject, EventTriggerType.PointerExit,delegate{OnExitInterface(gameObject);});
    }
    
    private void OnSlotUpdate(InventorySlotS _slot)
    {
        if (_slot.item.Id >= 0)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1? "": 
                _slot.amount.ToString("n0");
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    public abstract void CreateSlots();
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHovered = obj;
        FindObjectOfType<ItemInfo>().UpdateUIInformation(slotsOnInterface[obj].ItemObject);
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHovered = null;
        FindObjectOfType<ItemInfo>().UpdateUIInformation(null);
    }
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseOver = null;
    }
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemDragged = CreateTempItem(obj);
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemDragged);
        if (MouseData.interfaceMouseOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.slotHovered)
        {
            InventorySlotS mouseHoverSlotData = MouseData.interfaceMouseOver.slotsOnInterface[MouseData.slotHovered];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
    } 
        public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemDragged != null)
        {
            MouseData.tempItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
         public void OnClick(GameObject obj)
         {
             if (!slotsOnInterface.ContainsKey(obj) && slotsOnInterface[obj].item != null)
                 return;
             ConsumableObject consume = slotsOnInterface[obj].ItemObject as ConsumableObject;
             if (consume.GetType() == typeof(ConsumableObject))
             {
                 consume.ConsumePotion(this, obj);
             }
         }
 }
public static class MouseData
{
    public static UserInterface interfaceMouseOver;
    public static GameObject tempItemDragged;
    public static GameObject slotHovered;
    public static GameObject slotClicked;
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlotS> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlotS> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.Id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1? "": 
                    _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}


