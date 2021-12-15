using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTrigger
{
    OnDestroy,
    OnHealthZero
}


public class QuestTestEnemy : MonoBehaviour
{
    public string questCode;
    [SerializeField] TargetTrigger targetTrigger;
    public delegate void QuestTargetReachedDelegate(string theQuestCode);
    public static event QuestTargetReachedDelegate OnQuestTarget;
    
    private void Target()
    {
        if (OnQuestTarget != null)
        {
            OnQuestTarget(questCode);
        }
    }
    private void Update() 
    {
        if (targetTrigger == TargetTrigger.OnHealthZero)
        {
            if (GetComponent<Enemy>().Health <= 0)
            {
                Target();
            }
        }
    }
}
