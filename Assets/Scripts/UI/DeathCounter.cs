using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    private PlayerStats playerStats;
    private TextMeshProUGUI text;
    private string comment;
    
    private void Awake() 
    {
        text = GetComponent<TextMeshProUGUI>();
        playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
    }
    private void LateUpdate() 
    {
        switch (playerStats.DeathCount) 
        {
            case 0:
                comment = "Perfect Run! (so far)";
                break;
            case 1:
                comment = "Still Impressive";
                break;
            case 5:
                comment = "Hmm, maybe be a bit more careful";
                break;
            case 10:
                comment = "*Sigh* These filthy casuals..";
                break;
            case 15:
                comment = "No0b gettin' pwned here!";
                break;
            case 20:
                comment = "Grandpa Gaming inc";
                break;
            case 25:
                comment = "Sorry, we didn't make a baby-mode";
                break;
        }
        text.text = $"Deathcount: {playerStats.DeathCount} - {comment}";
    }
}