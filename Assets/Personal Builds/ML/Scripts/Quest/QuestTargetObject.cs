using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuestTargetObject : ScriptableObject
{
    public QuestType target;
    
    public delegate void TargetReachedDelegate();

    public static event TargetReachedDelegate OnTargetReached;
    
    
    private void OnDestroy()
    {
        
    }
}
