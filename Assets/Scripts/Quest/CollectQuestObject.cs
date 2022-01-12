using UnityEngine;

[CreateAssetMenu(fileName ="New Collect Quest", menuName = "QuestSystem/QuestObjects/CollectQuest")]
public class CollectQuestObject : QuestObject
{
    
    // Unused but to be attached to collectable quest objects.
    
    public CollectQuestObject()
    {
        questType = QuestType.Collect;
    }
}
