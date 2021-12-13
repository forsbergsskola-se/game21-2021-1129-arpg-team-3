using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class DialogueReader : MonoBehaviour
{
    [SerializeField] private Canvas dialoguePopup;
    [SerializeField] private DialogueContainer dialogueObject;
    [SerializeField] private QuestObject attachedQuest;
    
    private DialogueContainer dialogueContainer;
    private List<NodeLinkData> currentOutputNodes;
    
    private bool boxIsUp = false;
    private Canvas currentDialogue;

    private DialogueSystem dialogueSystem;
    private List<TextMeshProUGUI> texts;
    
    private string currentNodeGuid;

    private string currentNpcLine;

    private Transform playerTrans;

    private int clickCount;
    public delegate void TradeStartDelegate(); 
    public static event TradeStartDelegate OnStartTrade;
    
    public delegate void StartEndDialogueDelegate(); 
    public static event StartEndDialogueDelegate OnStartEndDialogue;
    
    public delegate void AcceptQuestDelegate(); 
    public static event AcceptQuestDelegate OnAcceptQuest;
    

    private void Start()
    {
        playerTrans = GameObject.FindWithTag("Player").transform;
    }


    private void AcceptQuest()
    {
        if (OnAcceptQuest != null)
        {
            OnAcceptQuest();
        }
    }

    private void StartTrade()
    {
        if (OnStartTrade != null)
        {
            OnStartTrade();
        }
    }
    
    private string GetFirstLineOfDialogue()
    {
        dialogueContainer =  dialogueObject; 
        currentNodeGuid = dialogueContainer.NodeLinks[0].TargetNodeGUID;
        string text = " ";

        foreach (var el in dialogueContainer.DialogueNodeData)
        {
            if (el.NodeGUID == currentNodeGuid)
            {
                text = el.DialogueText;
            }
        }

        return text;
    }

    private void OnMouseDown()
    {
        if (!boxIsUp && Vector3.Distance(transform.position, playerTrans.position) < 4)
        {
            boxIsUp = true;
            currentDialogue = Instantiate(dialoguePopup);
            texts = currentDialogue.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            texts[0].text = GetFirstLineOfDialogue();
            GetOutputNodesFromNode();
            SetupReplyButtons();
            
            texts[1].text = "Peasant";
        }

        if (OnStartEndDialogue != null && Vector3.Distance(transform.position, playerTrans.position) < 4)
        {
            OnStartEndDialogue();
        }
    }

    private void SetupReplyButtons()
    {
        clickCount = 0;
        var buttons = currentDialogue.GetComponentsInChildren<Button>().ToList();
       
        foreach (var button in buttons)
        {
            Button butt = button;
            butt.onClick.RemoveAllListeners();
            butt.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        for (int i = 0; i < currentOutputNodes.Count; i++)
        {
            int test = i;
            string targetGuid = currentOutputNodes[test].TargetNodeGUID;
            string chosenLine = currentOutputNodes[test].PortName;
            buttons[i].onClick.AddListener(() => ClickContinue(targetGuid, chosenLine));
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = i + 1 + "." + currentOutputNodes[i].PortName;
        }

    }

    private string GetDialogueFromNode(string guid)
    {
        string outDialogue = "";
        for (int i = 0; i < dialogueContainer.DialogueNodeData.Count; i++)
        {
            if (dialogueContainer.DialogueNodeData[i].NodeGUID == guid)
            {
                outDialogue = dialogueContainer.DialogueNodeData[i].DialogueText;
            }
        }

        return outDialogue;
    }
    
    private void GetOutputNodesFromNode()
    { 
       currentOutputNodes = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == currentNodeGuid).ToList();
    }

    private void ShutDownDialogue()
    {
    //    continueButton.onClick.RemoveListener(() =>ClickContinue(nextNodeGuid));
        Destroy(currentDialogue.gameObject);
        boxIsUp = false;

        if (OnStartEndDialogue != null)
        {
            OnStartEndDialogue();
        }
    }

    private void PauseDialogue(string condition)
    {
        currentDialogue.gameObject.SetActive(false);
        TradeSystem.OnEndTrade += ResumeDialogue;
        StartTrade();
    }

    private void ResumeDialogue()
    {
        currentDialogue.gameObject.SetActive(true);

        currentNodeGuid = currentOutputNodes[0].TargetNodeGUID;
        GetOutputNodesFromNode();
        SetupReplyButtons();
        texts[0].text = GetDialogueFromNode(currentNodeGuid);

        TradeSystem.OnEndTrade -= ResumeDialogue;
    }
    
    private void ClickContinue(string nextNodeGuid, string selectedLine)
    {
        if (clickCount < 1)
        {
            clickCount++;
        //    string currentLine = GetDialogueFromNode(currentNodeGuid);
            string nextLine = GetDialogueFromNode(nextNodeGuid);

            if (selectedLine == "Trade")
            {
                currentNodeGuid = nextNodeGuid;
                PauseDialogue(nextLine);
            }
            
            
            
            else
            {
                texts[0].text = nextLine;
                currentNodeGuid = nextNodeGuid;

                GetOutputNodesFromNode();
                SetupReplyButtons();

                if (selectedLine == "Leave")
                {
                    ShutDownDialogue();
                }
            }
        }
    }
}
