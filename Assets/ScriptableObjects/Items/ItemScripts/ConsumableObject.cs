using UnityEngine;
[CreateAssetMenu(fileName = "New Consumeable Object", menuName = "Inventory System/Items/Consumeable")]
public class ConsumableObject : ItemObject
{
    public PlayerStats playerStats;
    public int restoreHealthValue;
    public void Awake()
    {
       // type = ItemTypeS.Consumable;
    }
    public void ConsumePotion()
    {
       playerStats.Health =+ restoreHealthValue;
       Destroy(this);
    }
    public override void SetValuesFromTarget(ItemObject target)
    {
        base.SetValuesFromTarget(target);
        ConsumableObject targetConsume = (ConsumableObject) target;
        restoreHealthValue = targetConsume.restoreHealthValue;
    }
}
