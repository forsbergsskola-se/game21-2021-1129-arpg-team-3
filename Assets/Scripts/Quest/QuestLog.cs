using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName ="Quest Log", menuName = "QuestSystem/QuestObjects/QuestLog")]
public class QuestLog : ScriptableObject
{
    
    // To handle multiple quests.
    
    public List<QuestObject> quests = new();
}
