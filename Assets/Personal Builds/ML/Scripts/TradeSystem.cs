using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystem : MonoBehaviour
{
//    [SerializeField] private Canvas tradeCanvas;
    private Canvas useTradeCanvas;
    [SerializeField] private Canvas itemCanvas;
    [SerializeField] private RawImage itemPanel;
    private int itemCount;
    public delegate void EndTradeDelegate();
    public static event EndTradeDelegate OnEndTrade;

    void Start()
    {
        DialogueReader.OnStartTrade += StartToTrade;
        if (useTradeCanvas == null)
        {
            useTradeCanvas = Instantiate(itemCanvas);
            useTradeCanvas.gameObject.SetActive(false);
        }
    }

    private void CloseShop()
    {
        useTradeCanvas.GetComponentInChildren<Button>().onClick.RemoveListener(CloseShop);
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

        Debug.Log("start to trade");
        if (itemCount < 1)
        {
            AddItemToStore();
            itemCount++;
        }
    }


    private void AddItemToStore()
    {
       useTradeCanvas.GetComponentInChildren<List>().AddItem();
    }
}
