using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory")]
public class PlayerInventoryContainerTest : ScriptableObject
{
   public List<InventoryItem_ML> items = new();
}
