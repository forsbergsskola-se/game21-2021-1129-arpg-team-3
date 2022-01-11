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
    private readonly KeyCode openLogKey = KeyCode.F3;

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
        QuestTarget.OnQuestTarget += HitTarget;
    }

    private void HitTarget(QuestCode questCode)
    { 
      var quest = questLogObject.quests.Single(x => x.QuestCode == questCode);
      quest.numberTargetsGot++;

      if (quest.numberTargetsGot >= quest.NumberTargets)
      {
          if (quest.questState == QuestState.Accepted)
          {
              ChangeQuestState(questCode, QuestState.CompletedWithAccept);
          }
          else if (quest.questState == QuestState.NotAccepted)
          {
              ChangeQuestState(questCode, QuestState.CompletedWithoutAccept);
          }
          QuestComplete(quest.QuestCode);
      }
    }

    private void ChangeQuestState(QuestCode questCode, QuestState newState)
    {
        questLogObject.quests.Single(x => x.QuestCode == questCode).questState = newState;
    }

    private int CountActiveQuests()
    {
       return questLogObject.quests.Where(x => x.questState != QuestState.NotAccepted).ToList().Count;
    }
    
    
    private void AcceptQuest(QuestObject acceptedQuest)
    {
        ChangeQuestState(acceptedQuest.QuestCode, QuestState.Accepted);
        SetupQuestButton(questLogObject.quests.IndexOf(acceptedQuest), CountActiveQuests());
    }

    private void SetupQuestButton(int questIndex, int numberQuests)
    {
        Vector3 addVector = new Vector3(0, numberQuests * buttonIncrement);
        var panel = Instantiate(questLogPanel, keepQuest.transform);
        panel.GetComponent<RectTransform>().localPosition = buttonStartPos + addVector;
        panel.GetComponentInChildren<TextMeshProUGUI>().text = questLogObject.quests[questIndex].DisplayName;
        panel.GetComponentInChildren<Button>().onClick.AddListener(()=> SelectQuestButton(questIndex));
    }

    private void SelectQuestButton(int questIndex)
    {
        var texts = keepQuest.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = questLogObject.quests[questIndex].DisplayName;
        texts[1].text = questLogObject.quests[questIndex].QuestDescription;
        texts[2].text = $"You will receive {questLogObject.quests[questIndex].CashReward} gold";
        texts[3].text = questLogObject.quests[questIndex].numberTargetsGot +  "/" + questLogObject.quests[questIndex].NumberTargets;
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
        var texts = keepQuest.GetComponentsInChildren<TextMeshProUGUI>().ToList();

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
