using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTrigger
{
    OnDestroy
}


public class QuestTestEnemy : MonoBehaviour
{
    public string questCode;
    public delegate void QuestTargetReachedDelegate(string theQuestCode);
    public static event QuestTargetReachedDelegate OnQuestTarget;
    
    private void Target(string theQuestCode)
    {
        if (OnQuestTarget != null)
        {
            OnQuestTarget(theQuestCode);
        }
    }
    private void Update() {
        if (GetComponent<Enemy>().Health <= 0) {
            Target(questCode);
        }
    }
}
