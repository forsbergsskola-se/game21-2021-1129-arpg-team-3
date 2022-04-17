using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashCounter : MonoBehaviour
{
    private void Awake()
    {
        TradeInterface.OnUpdateGold += UpdateGold;
    }

    private void UpdateGold(int goldAmount)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"Gold: {goldAmount}";
    }
}
