using UnityEngine;

[CreateAssetMenu(fileName = "New KeyItem Object", menuName = "Inventory System/Items/KeyItems")]
public class KeyItemObject : ItemObject 
{

    public void Awake()
    {
        //type = ItemTypeS.KeyItem;
    }

    public override void SetValuesFromTarget(ItemObject target)
    {
        base.SetValuesFromTarget(target);
       // KeyItemObject targetKey = (KeyItemObject) target;
    }
}
