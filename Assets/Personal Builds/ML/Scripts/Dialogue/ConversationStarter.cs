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
    private bool boxIsUp = false;
    private Canvas currentDialogue;
    private Button continueButton;
    private int sentenceCount = 0;
    private DialogueSystem dialogueSystem;
    private List<TextMeshProUGUI> texts;

    private void OnMouseDown()
    {
        if (!boxIsUp)
        {
            boxIsUp = true;
            currentDialogue = Instantiate(dialoguePopup);
            texts = currentDialogue.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            continueButton = currentDialogue.GetComponentInChildren<Button>();

            continueButton.onClick.AddListener(ClickContinue);
            texts[0].text = dialogue.sentences[0];
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
