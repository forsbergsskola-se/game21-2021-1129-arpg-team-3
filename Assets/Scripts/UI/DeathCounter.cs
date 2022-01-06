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
            case 0:
                comment = "Perfect Run! (so far)";
                break;
            case 1:
                comment = "Still Impressive";
                break;
            case 5:
                comment = "Ah, you're a casual..";
                break;
            case 10:
                comment = "*Sigh*";
                break;
            case 15:
                comment = "No0b gettin pwned here!";
                break;
            case 20:
                comment = "Grandpa Gaming inc";
                break;
            case 25:
                comment = "Sorry, we didn't have time to implement a baby-mode";
                break;
        }
        if (playerStats.DeathCount <= 0) 
        {
            
        }
        text.text = $"Deathcount: {playerStats.DeathCount} - {comment}";
    }
}
