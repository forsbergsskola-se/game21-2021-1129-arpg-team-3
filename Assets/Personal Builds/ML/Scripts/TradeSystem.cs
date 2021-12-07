using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystem : MonoBehaviour
{
    [SerializeField] private Canvas tradeCanvas;
    private Canvas useTradeCanvas;

    public delegate void EndTradeDelegate();
    public static event EndTradeDelegate OnEndTrade;

    void Start()
    {
        useTradeCanvas = Instantiate(tradeCanvas);
        useTradeCanvas.gameObject.SetActive(false);
        DialogueReader.OnStartTrade += StartToTrade;
    }

    private void CloseShop()
    {
        useTradeCanvas.gameObject.SetActive(false);
        if (OnEndTrade != null)
        {
            OnEndTrade();
        }
    }
    
    private void StartToTrade()
    {
        useTradeCanvas.gameObject.SetActive(true);
        useTradeCanvas.GetComponentInChildren<Button>().onClick.AddListener(CloseShop);
    }
    
}
