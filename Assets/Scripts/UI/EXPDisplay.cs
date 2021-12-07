using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPDisplay : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Text _text;
    
    private void Awake() {
        _text = GetComponent<Text>();
        _playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void LateUpdate() {
        _text.text = $"LVL: {_playerStats.PlayerLevel} XP: {_playerStats.Experience} / {_playerStats.MaxExperience}";
    }
}
