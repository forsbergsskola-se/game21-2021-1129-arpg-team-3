using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPDisplay : MonoBehaviour
{
    private PlayerStats playerStats;
    private Text text;
    
    private void Awake() {
        text = GetComponent<Text>();
        playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void LateUpdate() {
        text.text = $"LVL: {(int)playerStats.PlayerLevel} XP: {(int)playerStats.Experience} / {(int)playerStats.MaxExperience}";
    }
}
