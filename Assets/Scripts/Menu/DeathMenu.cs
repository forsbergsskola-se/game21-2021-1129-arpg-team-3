using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public InventoryObjects inventory;
    public InventoryObjects equipment;
    private static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public PlayerStats playerStats;
    public PlayerController player;
    public SpawnControl spawnControl;

    void Update()
    {
        if (playerStats.PlayerDied)
        {
            Pause();
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        player.inDialogue = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        player.inDialogue = false;
        spawnControl.Respawn();
    }
    
    public void LoadMenu()
    {
        Debug.Log("Load me the menu now!");
        //Can load Main Menu scene on click when we add Menu scene
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting diz shiet");
        Application.Quit();
    }
}