using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
    public PlayerController player;
    public InventoryObjects inventory;
    public Dictionary<GameObject, InventorySlotS> slotsOnInterface = new Dictionary<GameObject, InventorySlotS>();
    void Start()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter,delegate{OnEnterInterface(gameObject);});
        AddEvent(gameObject, EventTriggerType.PointerExit,delegate{OnExitInterface(gameObject);});
    }
    void Update()
    {
        UpdateSlots();
    }

    public abstract void CreateSlots();
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlotS> _slot in slotsOnInterface)
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
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHovered = null;
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
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (slotsOnInterface[obj].item.Id >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;
        }

        MouseData.tempItemDragged = mouseObject;
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
            InventorySlotS mouseHoverSlotDate = MouseData.interfaceMouseOver.slotsOnInterface[MouseData.slotHovered];
           // inventory.SwapItems();
        }
    } 
        public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemDragged != null)
        {
            MouseData.tempItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

public static class MouseData
{
    public static UserInterface interfaceMouseOver;
    public static GameObject tempItemDragged;
    public static GameObject slotHovered;
}

