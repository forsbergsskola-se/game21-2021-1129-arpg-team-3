using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    private enum ItemObjectTypes
    {
        Equipment,
        Consumable,
        KeyItem,
        None
    }
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;
    public Image itemImage;
    //public TextMeshProUGUI armorValue;
   // public TextMeshProUGUI damageValue;
    public TextMeshProUGUI tierGrade;
    public TextMeshProUGUI buffs;
    public GameObject ItemInfoHolder;
    
    public void UpdateUIInformation(ItemObject itemObject)
    {
        if (itemObject)
        {
            ItemInfoHolder.SetActive(true);
            ItemObjectTypes objectTypes = DetectItemType(itemObject);
            switch (objectTypes)
            {
                case ItemObjectTypes.Equipment:
                {
                    EquipmentObject equipment = itemObject as EquipmentObject;
                    UpdateUIInfo(equipment.data.Name, equipment.description, equipment.uiDisplay, equipment.tierGrade, equipment.data.buffs);
                }
                    break;
                case ItemObjectTypes.Consumable:
                {
                    ConsumableObject consumable = itemObject as ConsumableObject;
                    UpdateUIInfo(consumable.data.Name, consumable.description, consumable.uiDisplay, "", consumable.data.buffs);
                }
                    break;
                case ItemObjectTypes.KeyItem:
                {
                    KeyItemObject keyItem = itemObject as KeyItemObject;
                    UpdateUIInfo(keyItem.data.Name, keyItem.description, keyItem.uiDisplay, 
                        "", keyItem.data.buffs);
                }
                    break;
                case ItemObjectTypes.None:
                    ItemInfoHolder.SetActive(false);
                    break;
            }
        }
        else
        {ItemInfoHolder.SetActive(false);}
    }

    private ItemObjectTypes DetectItemType(ItemObject itemType)
    {
        EquipmentObject equipment = itemType as EquipmentObject;
        if (equipment)
        {
            return ItemObjectTypes.Equipment;
        } 
        KeyItemObject keyItem = itemType as KeyItemObject;
        if (keyItem)
        {
            return ItemObjectTypes.KeyItem;
        }
        ConsumableObject consumable = itemType as ConsumableObject;
        if (consumable)
        {
            return ItemObjectTypes.Consumable;
        }

        return ItemObjectTypes.None;
    }
    private void UpdateUIInfo(string name, string descriptionText, Sprite itemImageDisplay, string tierText, ItemBuff[] buffInfo)
    {
        nameText.text = name;
        description.text = descriptionText;
        itemImage.sprite = itemImageDisplay;
        tierGrade.text = tierText;
        buffs.text = ConvertBuffInfoToString(buffInfo);
    }
    private string ConvertBuffInfoToString(ItemBuff[] itemData)
    {
        string cleanupName = "";
        for (int i = 0; i < itemData.Length; i++)
        {
            ItemBuff buff = itemData[i];
            string buffInfo = $"{buff.attribute} : {buff.value}";
            if (string.IsNullOrEmpty(cleanupName))
            {
                cleanupName = buffInfo;
            }
            else
            {
                cleanupName = cleanupName + "\n" + buffInfo;
            }
        }
        return cleanupName;
    }
}
