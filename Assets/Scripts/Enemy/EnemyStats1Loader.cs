using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats1Loader : MonoBehaviour
{
    public EnemyStats1 enemyStats1;
    private EnemyHealthBar enemyHealthBar;
    private AI ai;

    private void Start() {
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        ai = GetComponent<AI>();
    }

    private void Update() {
        ToggleHealthBar();
    }
    private void ToggleHealthBar() {
        if (Physics.Raycast(GetCursorPosition(), out var hitInfo)) {
            if (hitInfo.collider.CompareTag("Enemy") || ai.showHealthBar) {
                enemyHealthBar.gameObject.SetActive(true);
            }
            else {
                enemyHealthBar.gameObject.SetActive(false);
            }
        }
    }
    Ray GetCursorPosition() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Fires ray
        return ray;
    }
}
