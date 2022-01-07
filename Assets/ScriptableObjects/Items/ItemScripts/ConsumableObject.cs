using UnityEngine;
[CreateAssetMenu(fileName = "New Consumeable Object", menuName = "Inventory System/Items/Consumeable")]
public class ConsumableObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
       // type = ItemTypeS.Consumable;
    }

    public override void SetValuesFromTarget(ItemObject target)
    {
        base.SetValuesFromTarget(target);
        ConsumableObject targetConsume = (ConsumableObject) target;
        restoreHealthValue = targetConsume.restoreHealthValue;
    }
}
