using UnityEditor;
using UnityEngine;


public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    public ItemObject pickupItem()
    {
        ItemObject newItem = ScriptableObject.CreateInstance<ItemObject>();
        newItem.SetValuesFromTarget(item);
        return newItem;
    }
    
    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }

    public void OnAfterDeserialize()
    {
    }
}
