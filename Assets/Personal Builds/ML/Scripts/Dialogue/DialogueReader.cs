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
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Canvas dialoguePopup;
    [SerializeField] private ScriptableObject dialogueObject;
    [SerializeField] private Canvas AdditionalCanvas;
    
    private DialogueContainer dialogueContainer;
    private List<NodeLinkData> currentOutputNodes;
    
    
    
    private bool boxIsUp = false;
    private Canvas currentDialogue;
    private Button continueButton;

    private DialogueSystem dialogueSystem;
    private List<TextMeshProUGUI> texts;
    
    private string currentNodeGuid;
    
    public delegate void TradeStartDelegate();

    public static event TradeStartDelegate OnStartTrade;


    private void StartTrade()
    {
        if (OnStartTrade != null)
        {
            OnStartTrade();
        }
    }
    
    private string GetFirstLineOfDialogue()
    {
        dialogueContainer = (DialogueContainer) dialogueObject; 
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

    private List<string> GetAllChoicesFromNode(string nodeGuid)
    {
        List<string> outList = new List<string>();

        foreach (var el in dialogueContainer.NodeLinks)
        {
            if (el.BaseNodeGUID == nodeGuid)
            {
                outList.Add(el.PortName);
            }
        }

        return outList;
    }
    
    
    private void OnMouseDown()
    {
        
        if (!boxIsUp)
        {
            boxIsUp = true;
            currentDialogue = Instantiate(dialoguePopup);
            texts = currentDialogue.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            continueButton = currentDialogue.GetComponentInChildren<Button>();
            
            texts[0].text = GetFirstLineOfDialogue();
            GetOutputNodesFromNode();
            SetupReplyButtons();
            
            texts[1].text = dialogue.name;
        }
    }

    private void SetupReplyButtons()
    {
        
        var buttons = currentDialogue.GetComponentsInChildren<Button>().ToList();

        foreach (var button in buttons)
        {
            Button butt = button;
            butt.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        for (int i = 0; i < currentOutputNodes.Count; i++)
        {
            int test = i;
            buttons[i].onClick.AddListener(() => ClickContinue(currentOutputNodes[test].TargetNodeGUID));
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
        GetOutputNodesFromNode();
        SetupReplyButtons();
        ClickContinue(currentNodeGuid);
        TradeSystem.OnEndTrade -= ResumeDialogue;
    }
    
    private void ClickContinue(string nextNodeGuid)
    {
        string nextLine = GetDialogueFromNode(nextNodeGuid);
        
      
        if (nextLine == "TRADE")
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
            
            if (nextLine == "LEAVE")
            {
                ShutDownDialogue();
            }
        }
    }
}
