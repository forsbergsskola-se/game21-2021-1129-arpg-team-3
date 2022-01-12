
using UnityEngine;

[CreateAssetMenu(fileName ="New Kill Quest", menuName = "QuestSystem/QuestObjects/KillQuest")]
public class KillQuestObject : QuestObject
{
    
    // To be attached to destroyable quest objects.
    
    public KillQuestObject()
    {
        questType = QuestType.Kill;
    }
}
