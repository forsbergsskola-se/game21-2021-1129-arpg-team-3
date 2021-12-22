using UnityEngine;

public class OnSlotFunction : MonoBehaviour
{
   private ItemObject _itemObject;
   private ItemInfo _itemInfo;
   public void OnSetUp(ItemObject parent)
   {
      _itemObject = parent;
   }

   public void OnSlotClick()
   {
      _itemInfo = FindObjectOfType<ItemInfo>();
      _itemInfo.UpdateUIInformation(_itemObject);
      Debug.Log(_itemObject);
   }
}
