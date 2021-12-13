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
    
    void Start()
    {
        questLogObject.quests.Clear();
        keepQuest = Instantiate(questLogCanvas);
        keepQuest.gameObject.SetActive(false);
        DialogueReader.OnAcceptQuest += AcceptQuest;
    }

    private void AcceptQuest(QuestObject acceptedQuest)
    {
        questLogObject.quests.Add(acceptedQuest);
        SetupQuestButton(0);
        
        WriteToLog();
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
        keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[0].text = questLogObject.quests[questIndex].questName;
        keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[1].text = questLogObject.quests[questIndex].questDescription;
    }
    
    private void OnApplicationQuit()
    {
        questLogObject.quests.Clear();
        keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "";
        keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "";
    }

    private void WriteToLog()
    {
    //    keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[0].text = questLogObject.quests[0].questName;
    //    keepQuest.GetComponentsInChildren<TextMeshProUGUI>()[1].text = questLogObject.quests[0].questDescription;
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
