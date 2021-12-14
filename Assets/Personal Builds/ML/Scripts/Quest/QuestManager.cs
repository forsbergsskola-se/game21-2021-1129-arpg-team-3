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
    private readonly Vector3 buttonStartPos = new (-158, 168);
    private readonly float buttonIncrement = 7;
    [SerializeField] private RawImage questLogPanel;
    private Canvas keepQuest;
    private KeyCode openLogKey = KeyCode.F3;
    
    public delegate void QuestCompletedDelegate(string questCode);

    public static event QuestCompletedDelegate OnQuestComplete;


    private void QuestComplete(string questCode)
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

    private void HitTarget(string questCode)
    {
        foreach (var el in questLogObject.quests)
        {
            if (el.questCode == questCode)
            {
                el.numberTargetsGot++;

                if (el.numberTargetsGot >= el.numberTargets)
                {
                    QuestComplete(el.questCode);
                }
            }
        }
    }

    private void RegisterAcceptQuest(string questCode)
    {
         questLogObject.quests.Where(x =>
         {
             if (x.questCode == questCode)
             {
                 x.state = QuestState.Accepted;
                 return true;
             }
             return false;
        });
    }
    
    private void AcceptQuest(QuestObject acceptedQuest)
    {
        acceptedQuest.numberTargetsGot = 0;
        questLogObject.quests.Add(acceptedQuest);
        SetupQuestButton(questLogObject.quests.Count - 1);
    }

    private void SetupQuestButton(int questIndex)
    {
        Vector3 addVector = new Vector3(0, questIndex * buttonIncrement);
        
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
            el.ResetTargets();
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
        questLogObject.quests.Clear();
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
