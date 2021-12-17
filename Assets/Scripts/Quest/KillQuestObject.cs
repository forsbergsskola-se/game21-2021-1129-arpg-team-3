
using UnityEngine;

[CreateAssetMenu(fileName ="New Kill Quest", menuName = "QuestSystem/QuestObjects/KillQuest")]
public class KillQuestObject : QuestObject
{
    public KillQuestObject()
    {
        questType = QuestType.Kill;
    }
}
