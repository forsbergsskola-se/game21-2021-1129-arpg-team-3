using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTestEnemy : MonoBehaviour
{
    [SerializeField] private string questCode;
    [SerializeField] private QuestTargetObject targetObject;
    
    public delegate void QuestTargetReachedDelegate(string theQuestCode);

    public static event QuestTargetReachedDelegate OnQuestTarget;

    private void Target(string theQuestCode)
    {
        if (OnQuestTarget != null)
        {
            OnQuestTarget(theQuestCode);
        }
            
    }
    
    private void OnMouseDown()
    {
        Target(questCode);
        Destroy(gameObject);
    }

}
