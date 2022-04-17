using System;
using UnityEngine;

[Flags]
public enum QuestCode
{
    Kill1,
    Collect1,
    Boss1
}

public enum QuestType
{
    Kill,
    Collect,
   // Go
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
    public QuestType questType;
    public QuestState questState;
    [SerializeField] QuestCode questCode;

    [SerializeField] int numberTargets;
    public int numberTargetsGot;
    
    [SerializeField] string displayName;
    
    public InventoryItemObject_ML itemReward;
    [SerializeField] int cashReward;
    [SerializeField] int experienceReward;
    
    [TextArea(15, 20)] 
    [SerializeField] string questDescription;
    
    public QuestCode QuestCode => questCode;
    public string DisplayName => displayName;
    public string QuestDescription => questDescription;
    public int CashReward => cashReward;
    public int ExperienceReward => experienceReward;
    public int NumberTargets => numberTargets;


    public void ResetQuest()
    {
        numberTargetsGot = 0;
        questState = QuestState.NotAccepted;
    }
}
