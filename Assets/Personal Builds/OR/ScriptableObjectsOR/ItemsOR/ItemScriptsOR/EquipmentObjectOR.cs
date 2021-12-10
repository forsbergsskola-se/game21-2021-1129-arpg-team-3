using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment ObjectOR", menuName = "Inventory SystemOR/Items/EquipmentOR")]
public class EquipmentObjectOR : ItemObjectOR
{
    public int armourValue;
    public int damageBonus;
    public string tierGrade;
    public void Awake()
    {
        type = ItemTypeOR.Equipment;
    }
}
