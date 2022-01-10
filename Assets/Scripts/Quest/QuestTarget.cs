using UnityEngine;

public enum TargetTrigger
{
    OnDestroy,
    OnHealthZero,
   // None
}

public class QuestTarget : MonoBehaviour
{
    public bool isFinalEnemy;

    public QuestCode questCode;
    
    [SerializeField] TargetTrigger targetTrigger;
    public delegate void QuestTargetReachedDelegate(QuestCode theQuestCode);
    public static event QuestTargetReachedDelegate OnQuestTarget;
    
    private void Target()
    {
        if (OnQuestTarget != null)
        {
            OnQuestTarget(questCode);
        }
    }
    //
    // private void OnMouseDown()
    // {
    //     if (targetTrigger == TargetTrigger.OnDestroy)
    //     {
    //         Target();
    //         Destroy(gameObject);
    //     }
    // }
    
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
