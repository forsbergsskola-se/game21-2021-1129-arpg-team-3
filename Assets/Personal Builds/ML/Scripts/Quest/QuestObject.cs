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
}


[CreateAssetMenu(fileName ="New Kill Quest", menuName = "QuestSystem/QuestObjects/KillQuest")]
public class KillQuestObject : QuestObject
{
    public KillQuestObject()
    {
        type = QuestType.Kill;
    }
}

[CreateAssetMenu(fileName ="New Collect Quest", menuName = "QuestSystem/QuestObjects/CollectQuest")]
public class CollectQuestObject : QuestObject
{
    public CollectQuestObject()
    {
        type = QuestType.Collect;
    }
}
