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
    public void ConsumePotion(UserInterface userInterface, GameObject obj)
    {
        if (!playerStats)
        {
            playerStats = FindObjectOfType<PlayerStatsLoader>().playerStats;
        }

        if (playerStats.Health == playerStats.GetModifiedStats().MaxHealth)
            return;
        playerStats.Health += restoreHealthValue;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Item/PotionUse");
        userInterface.slotsOnInterface[obj].RemoveItem();
    }
    public override void SetValuesFromTarget(ItemObject target)
    {
        base.SetValuesFromTarget(target);
        ConsumableObject targetConsume = (ConsumableObject) target;
        restoreHealthValue = targetConsume.restoreHealthValue;
    }
}
