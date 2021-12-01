using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationHandler : MonoBehaviour
{
    private TextMeshProUGUI TheText;
    
    void Start()
    {
        TheText = GetComponent<TextMeshProUGUI>();

        GetComponent<TextMeshProUGUI>().text = "Hello you";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
