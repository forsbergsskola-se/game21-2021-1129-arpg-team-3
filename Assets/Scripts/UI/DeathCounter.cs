using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    private PlayerStats playerStats;
    private Text text;
    private string comment;
    
    private void Awake() {
        text = GetComponent<Text>();
        playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void LateUpdate() {
        switch (playerStats.DeathCount) 
        {
            case 1:
                comment = "Perfect Run! (so far)";
                break;
            case 2:
                comment = "First Blood!";
                break;
        }
        if (playerStats.DeathCount <= 0) 
        {
            
        }
        text.text = $"Deathcount: {playerStats.DeathCount} - {comment}";
    }
}
