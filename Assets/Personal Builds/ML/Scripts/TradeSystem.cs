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
        AddItemToStore();
    }


    private void AddItemToStore()
    {
       RectTransform itemContainer = useTradeCanvas.GetComponentInChildren<RectTransform>();

       
       var newItem = Instantiate(itemPanel, itemContainer);
       
    //   newItem.transform.SetParent(itemContainer, false);
    }
}
