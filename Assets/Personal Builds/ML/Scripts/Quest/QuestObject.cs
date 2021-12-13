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
}

public class KillQuestObject : QuestObject
{
    public KillQuestObject()
    {
        type = QuestType.Kill;
    }
}

public class CollectQuestObject : QuestObject
{
    public CollectQuestObject()
    {
        type = QuestType.Collect;
    }
}
