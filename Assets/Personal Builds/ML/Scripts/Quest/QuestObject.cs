using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Kill,
    Collect,
    Go
}

public abstract class QuestObject : ScriptableObject
{
    public QuestType type;
    public string questCode;
    public int numberTargets;
    public string questName;
    public InventoryItemObject itemReward;
    public int cashReward;
    
    [TextArea(15, 20)] 
    public string questDescription;
}
