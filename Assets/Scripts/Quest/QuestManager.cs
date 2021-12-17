using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Canvas questLogCanvas;
    [SerializeField] private QuestLog questLogObject;
    [SerializeField] private RawImage questLogPanel;
    
    private Canvas keepQuest;
    private KeyCode openLogKey = KeyCode.F3;

    private readonly Vector3 buttonStartPos = new (-158, 168);
    private readonly float buttonIncrement = 7;

    public delegate void QuestCompletedDelegate(QuestCode questCode);

    public static event QuestCompletedDelegate OnQuestComplete;


    private void QuestComplete(QuestCode questCode)
    {
        if (OnQuestComplete != null)
        {
            OnQuestComplete(questCode);
        }
    }

    void Start()
    {
        keepQuest = Instantiate(questLogCanvas);
        keepQuest.gameObject.SetActive(false);
        DialogueReader.OnAcceptQuest += AcceptQuest;
        QuestTestEnemy.OnQuestTarget += HitTarget;
    }

    private void HitTarget(QuestCode questCode)
    {
        foreach (var el in questLogObject.quests)
        {
            if (el.questCode == questCode)
            {
                el.numberTargetsGot++;
                
                if (el.numberTargetsGot >= el.numberTargets)
                {
                    if (el.state == QuestState.Accepted)
                    {
                        ChangeQuestState(questCode, QuestState.CompletedWithAccept);
                    }
                    else if (el.state == QuestState.NotAccepted)
                    {
                        ChangeQuestState(questCode, QuestState.CompletedWithoutAccept);
                    }
                    
                    QuestComplete(el.questCode);
                }
            }
        }
    }

    private void ChangeQuestState(QuestCode questCode, QuestState newState)
    {
        foreach (var el in questLogObject.quests)
        {
            if (el.questCode == questCode)
            {
                el.state = newState;
            }
        }
    }

    private int CountActiveQuests()
    {
       return questLogObject.quests.Where(x => x.state != QuestState.NotAccepted).ToList().Count;
    }
    
    
    private void AcceptQuest(QuestObject acceptedQuest)
    {
        ChangeQuestState(acceptedQuest.questCode, QuestState.Accepted);
        SetupQuestButton(questLogObject.quests.IndexOf(acceptedQuest), CountActiveQuests());
    }

    private void SetupQuestButton(int questIndex, int numberQuests)
    {
        Vector3 addVector = new Vector3(0, numberQuests * buttonIncrement);
        
        var panel = Instantiate(questLogPanel, keepQuest.transform);
        panel.GetComponent<RectTransform>().localPosition = buttonStartPos + addVector;
        panel.GetComponentInChildren<TextMeshProUGUI>().text = questLogObject.quests[questIndex].questName;
        panel.GetComponentInChildren<Button>().onClick.AddListener(()=> SelectQuestButton(questIndex));
    }

    private void SelectQuestButton(int questIndex)
    {
        var texts = keepQuest.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = questLogObject.quests[questIndex].questName;
        texts[1].text = questLogObject.quests[questIndex].questDescription;
        texts[2].text = "You Will receive " + questLogObject.quests[questIndex].cashReward + " gold";
        texts[3].text = questLogObject.quests[questIndex].numberTargetsGot +  "/" + questLogObject.quests[questIndex].numberTargets;
    }

    private void ResetLog()
    {
        foreach (var el in questLogObject.quests)
        {
            el.ResetQuest();
        }
    }
    
    private void ResetLogTexts()
    {
        var texts = keepQuest.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < 4; i++)
        {
            texts[i].text = " ";
        }
    }
    
    private void OnApplicationQuit()
    {
        ResetLog();
    }
    
    
    private void ActivateQuestLog()
    {
        if (!keepQuest.isActiveAndEnabled)
        {
            keepQuest.gameObject.SetActive(true);
            ResetLogTexts();
        }
        else
        {
            keepQuest.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(openLogKey))
        {
            ActivateQuestLog();
        }
    }
}
