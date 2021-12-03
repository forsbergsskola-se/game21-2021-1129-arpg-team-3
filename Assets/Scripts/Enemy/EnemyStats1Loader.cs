using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats1Loader : MonoBehaviour
{
    public EnemyStats1 enemyStats1;
    public EnemyHealthBar enemyHealthBar;
    
    private void Update() {
        ToggleHealthBar();
    }
    private void ToggleHealthBar() {
        if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                enemyHealthBar.gameObject.SetActive(true);
            }
        }
    }
    Ray GetCursorPosition() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
        return ray;
    }
}
