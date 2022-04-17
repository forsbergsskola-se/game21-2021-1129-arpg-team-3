using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPDisplay : MonoBehaviour
{
    
    // Shows player XP in text.
    
    private PlayerStats playerStats;
    private TextMeshProUGUI text;
    
    private void Awake() 
    {
        text = GetComponent<TextMeshProUGUI>();
        playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void LateUpdate() 
    {
        text.text = $"LVL: {(int)playerStats.PlayerLevel} | XP: {(int)playerStats.Experience} / {(int)playerStats.MaxExperience}";
    }
}
