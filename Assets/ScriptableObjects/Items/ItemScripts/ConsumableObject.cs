using UnityEngine;
[CreateAssetMenu(fileName = "New Consumeable Object", menuName = "Inventory System/Items/Consumeable")]
public class ConsumableObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
       // type = ItemTypeS.Consumable;
    }
}
