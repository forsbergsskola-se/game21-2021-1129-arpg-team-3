using UnityEngine;

[CreateAssetMenu(fileName ="New Collect Quest", menuName = "QuestSystem/QuestObjects/CollectQuest")]
public class CollectQuestObject : QuestObject
{
    public CollectQuestObject()
    {
        questType = QuestType.Collect;
    }
}
