using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public int armourValue;
    public int damageBonus;
    public string tierGrade;
    public void Awake()
    {
        type = ItemTypeS.Chest;
    }
}
