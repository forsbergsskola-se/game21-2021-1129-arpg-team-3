using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeInterface : UserInterface
{
    public GameObject inventoryPrefab;
    public GameObject itemInfoDisplay;
    private readonly int X_START = -100;
    private readonly int Y_START = 165;
    private readonly int X_SPACE_BETWEEN_ITEM = 65;
    private readonly int NUMBER_OF_COLUMN = 4;
    private readonly int Y_SPACE_BETWEEN_ITEM = 50;
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlotS>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerEnter,delegate{OnEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit,delegate{OnExit(obj);});
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM *(i % NUMBER_OF_COLUMN)),
            Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}