using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Canvas dialoguePopup;
    [SerializeField] private ScriptableObject dialogueObject;
    
    private DialogueContainer dialogueContainer;
    
    private bool boxIsUp = false;
    private Canvas currentDialogue;
    private Button continueButton;
    private int sentenceCount = 0;
    private DialogueSystem dialogueSystem;
    private List<TextMeshProUGUI> texts;

    private string GetFirstLineOfDialogue()
    {
        dialogueContainer = (DialogueContainer) dialogueObject; 
        string firstGuid = dialogueContainer.NodeLinks[0].TargetNodeGUID;
        string text = " ";

        foreach (var el in dialogueContainer.DialogueNodeData)
        {
            if (el.NodeGUID == firstGuid)
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
    
    private void GetNodeWithDepth()
    {
        string firstGuid = dialogueContainer.NodeLinks[0].TargetNodeGUID;
    }
    
    private void OnMouseDown()
    {
        
        if (!boxIsUp)
        {
            boxIsUp = true;
            currentDialogue = Instantiate(dialoguePopup);
            texts = currentDialogue.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            continueButton = currentDialogue.GetComponentInChildren<Button>();

            continueButton.onClick.AddListener(ClickContinue);
            texts[0].text = GetFirstLineOfDialogue();
            texts[1].text = dialogue.name;
            sentenceCount++;
        }
    }

    private void ClickContinue()
    {
        if (dialogue.sentences.Length > sentenceCount)
        {
            texts[0].text = dialogue.sentences[sentenceCount];
            
            if (sentenceCount == dialogue.sentences.Length - 1)
            {
                texts[2].text = "Leave";
            }
            sentenceCount++;
        }
        else
        {
            continueButton.onClick.RemoveListener(ClickContinue);
            Destroy(currentDialogue.gameObject);
            sentenceCount = 0;
            boxIsUp = false;
        }
    }
}
