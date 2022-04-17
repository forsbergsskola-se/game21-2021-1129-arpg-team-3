using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour 
{
	
	// Shows large health bar for the player on the UI. (unlike enemies which are in world space)
	
	public Slider slider;
	public Gradient gradient;
	public Image _fill;
	private PlayerStats _playerStats;
	
	private void Awake()
	{ 
		_playerStats = FindObjectOfType<PlayerStatsLoader>().playerStats;
	}

	private void LateUpdate() 
	{
		SetMaxHealth();
		ChangeHealthBar();
	}

	private void SetMaxHealth() 
	{
		slider.maxValue = _playerStats.MaxHealth;
		slider.value = _playerStats.Health;
		_fill.color = gradient.Evaluate(1f);
	}

	private void ChangeHealthBar() 
	{
		slider.value = _playerStats.Health;
		_fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
