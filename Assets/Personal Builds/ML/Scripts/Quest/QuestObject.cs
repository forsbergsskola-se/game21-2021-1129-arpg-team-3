using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Kill,
    Collect,
    Go
}

public enum QuestState
{
    NotAccepted,
    Accepted,
    CompletedWithoutAccept,
    CompletedWithAccept
}

public abstract class QuestObject : ScriptableObject
{
    public QuestType type;
    public QuestState state;
    
    public string questCode;
    
    public int numberTargets;
    public int numberTargetsGot;
    
    public string questName;
    
    public InventoryItemObject itemReward;
    public int cashReward;
    public int XPReward;
    
    [TextArea(15, 20)] 
    public string questDescription;
}
