using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Canvas questLogCanvas;
    [SerializeField] private QuestLog questLogObject;
    private readonly Vector3 buttonStartPos = new (-158, 168);
    [SerializeField] private RawImage questLogPanel;
    private Canvas keepQuest;
    
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
        questLogObject.quests.Clear();
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
    
    private void SetupReward(int questIndex)
    {
        
    }
    
    private void AcceptQuest(QuestObject acceptedQuest)
    {
        questLogObject.quests.Add(acceptedQuest);
        SetupQuestButton(0);
    }

    private void SetupQuestButton(int questIndex)
    {
        var panel = Instantiate(questLogPanel, keepQuest.transform);
        panel.GetComponent<RectTransform>().localPosition = buttonStartPos;
        panel.GetComponentInChildren<TextMeshProUGUI>().text = questLogObject.quests[questIndex].questName;
        panel.GetComponentInChildren<Button>().onClick.AddListener(()=> TestButtonClick(questIndex));
    }

    private void TestButtonClick(int questIndex)
    {
        var texts = keepQuest.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = questLogObject.quests[questIndex].questName;
        texts[1].text = questLogObject.quests[questIndex].questDescription;
        texts[2].text = "You Will receive " + questLogObject.quests[questIndex].cashReward + " gold";
        texts[3].text = questLogObject.quests[questIndex].numberTargetsGot +  "/" + questLogObject.quests[questIndex].numberTargets;

    }
    
    private void OnApplicationQuit()
    {
        questLogObject.quests.Clear();
        keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "";
        keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "";

        foreach (var el in questLogObject.quests)
        {
            el.numberTargetsGot = 0;
        }
    }
    
    
    private void ActivateQuestLog()
    {
        if (!keepQuest.isActiveAndEnabled)
        {
            keepQuest.gameObject.SetActive(true);
        }
        else
        {
            keepQuest.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ActivateQuestLog();
        }
    }
}
