using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystem : MonoBehaviour
{
    private Canvas useTradeCanvas;
    [SerializeField] private Canvas itemCanvas;
    [SerializeField] private RawImage itemPanel;
    [SerializeField] private List<InventoryItemObject_ML> merchantInventory;
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
        useTradeCanvas.GetComponentsInChildren<Button>()[0].onClick.RemoveListener(CloseShop);
        useTradeCanvas.gameObject.SetActive(false);
        if (OnEndTrade != null)
        {
            OnEndTrade();
        }
    }
    
    private void StartToTrade()
    {
        useTradeCanvas.gameObject.SetActive(true);
        useTradeCanvas.GetComponentsInChildren<Button>()[0].onClick.AddListener(CloseShop);
        
        AddItemToStore();
    }

    private void SetupAButton()
    {
        useTradeCanvas.GetComponentsInChildren<Button>()[1].onClick.AddListener(BuyButton);
    }

    private void BuyButton()
    {
        
    }

    private void AddItemToStore()
    {
       useTradeCanvas.GetComponentInChildren<StoreList>().AddItems();
    }
}
