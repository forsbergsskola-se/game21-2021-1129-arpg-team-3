using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Canvas dialoguePopup;
    private bool boxIsUp = false;
    private Canvas currentDialogue;
    
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (!boxIsUp)
        {
            boxIsUp = true;
            currentDialogue = Instantiate(dialoguePopup);

            TextMeshProUGUI TheText = GetComponent<TextMeshProUGUI>();
            List<TextMeshProUGUI> texts = currentDialogue.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            
            texts[0].text = dialogue.sentences[0];
            texts[1].text = dialogue.name;
        }
    }

    private void OnMouseOver()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
