using UnityEngine;

public enum ItemTypeS 
{
    Consumable,
    Helmet,
    Chest,
    Weapon,
    Legs,
    KeyItem
}

public enum Attributes
{
    AtkSpd,
    Damage,
    Health,
    Armor
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    
    public Sprite uiDisplay;
    public bool stackable;
    public int baseValue;
    public ItemTypeS type;
    [TextArea(15,20)]
    public string description;
    public Item data = new Item();
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    public virtual void SetValuesFromTarget(ItemObject target)
    {
        uiDisplay = target.uiDisplay;
        stackable = target.stackable;
        baseValue = target.baseValue;
        type = target.type;
        description = target.description;
        data = target.data;
    }
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
            buffs[i].attribute = item.data.buffs[i].attribute;
        }
        item.data.buffs = buffs;
    }
}
[System.Serializable]
public class ItemBuff: IModifiers
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
}
