using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
	
    [SerializeField] Image fill;
    private Enemy enemy;

    private void Awake() 
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void Update() 
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void LateUpdate() 
    {
        SetMaxHealth();
        ChangeHealthBar();
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
}
