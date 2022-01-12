using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour 
{
	// Shows the health bar of the enemy
	
	[SerializeField] Slider slider;
	[SerializeField] Gradient gradient;
	[SerializeField] Image fill;
	private Enemy enemy;
	private AI ai;

	private void Awake() 
	{
		enemy = GetComponentInParent<Enemy>();
		ai = GetComponentInParent<AI>();
	}
	private void Update() 
	{
		transform.rotation = Camera.main.transform.rotation;
	}
	// Sets health bar off by default. Turns it on only if the player mouse over the enemy or if the enemy sees the player.
	private void LateUpdate() 
	{
		DisableHealthBar();
		Ray GetCursorPosition() 
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			return ray;
		}
		if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) 
		{
			if (hitInfo.collider.CompareTag("Enemy") || ai.showHealthBar) 
			{
				SetMaxHealth();
				ChangeHealthBar();
			}
		}
	}

	private void SetMaxHealth() 
	{
		slider.maxValue = enemy.maxHealth;
		slider.value = enemy.Health;
		fill.color = gradient.Evaluate(1f);
	}
	private void ChangeHealthBar() 
	{
		slider.value = enemy.Health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
	private void DisableHealthBar() 
	{
		slider.value = 0;
		fill.color = Color.clear;
	}
}
