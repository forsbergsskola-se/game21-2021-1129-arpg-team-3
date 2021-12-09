using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    private PlayerStats playerStats;
    private Text text;
    
    private void Awake() {
        text = GetComponent<Text>();
        playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void LateUpdate() {
        text.text = $"Gold: {playerStats.Gold}";
    }
}
