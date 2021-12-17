using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestCode
{
    KILL1,
    COLLECT1,
    BOSS1
}

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
    public QuestCode questCode;
    
//    public string questCode;
    
    public int numberTargets;
    public int numberTargetsGot;
    
    public string questName;
    
    public InventoryItemObject_ML itemReward;
    public int cashReward;
    public int XPReward;
    
    [TextArea(15, 20)] 
    public string questDescription;

    public bool HasQuestCode(QuestCode testCode)
    {
        return questCode == testCode;
    }
    
    public void ResetQuest()
    {
        numberTargetsGot = 0;
        state = QuestState.NotAccepted;
    }
}
