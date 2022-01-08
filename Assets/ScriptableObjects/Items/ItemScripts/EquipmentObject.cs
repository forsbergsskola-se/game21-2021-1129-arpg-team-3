using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
   // public int armourBonus;
  //  public int damageBonus;
    public string tierGrade;
    public void Awake()
    {
        //type = ItemTypeS.Chest;
    }

    public override void SetValuesFromTarget(ItemObject target)
    {
        base.SetValuesFromTarget(target);
        EquipmentObject targetEquip = (EquipmentObject) target;
        tierGrade = targetEquip.tierGrade;
    }
}
