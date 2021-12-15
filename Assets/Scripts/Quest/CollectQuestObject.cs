using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Collect Quest", menuName = "QuestSystem/QuestObjects/CollectQuest")]
public class CollectQuestObject : QuestObject
{
    public CollectQuestObject()
    {
        type = QuestType.Collect;
    }
}
