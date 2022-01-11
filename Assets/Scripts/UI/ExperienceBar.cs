using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour 
{
	public Slider slider;
	public Gradient gradient;
	public Image _fill;
	private PlayerStats _playerStats;
	
	private void Awake()
	{ 
		_playerStats = GetComponentInParent<PlayerStatsLoader>().playerStats;
	}

	private void LateUpdate() 
	{
		SetMaxExperience();
		ChangeExperienceBar();
	}

	private void SetMaxExperience() 
	{
		slider.maxValue = _playerStats.MaxExperience;
		slider.value = _playerStats.Experience;
		_fill.color = gradient.Evaluate(1f);
	}

	private void ChangeExperienceBar() 
	{
		slider.value = _playerStats.Experience;
		_fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
