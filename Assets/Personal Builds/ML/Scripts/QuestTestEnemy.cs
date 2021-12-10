using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTestEnemy : MonoBehaviour
{
    [SerializeField] private QuestTarget target;
    
    public delegate void QuestTargetReachedDelegate();

    public static event QuestTargetReachedDelegate OnQuestTarget;

    private void Target()
    {
        if (OnQuestTarget != null)
        {
            OnQuestTarget();
        }
            
    }
    
    private void OnMouseDown()
    {
        Target();
        Destroy(gameObject);
    }

}
