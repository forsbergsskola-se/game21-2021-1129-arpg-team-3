using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public InventoryObjects inventory;
    public InventoryObjects equipment;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void SaveGame()
    {
        inventory.Save();
        equipment.Save();
    }
    public void LoadGame()
    {
        inventory.Load();
        equipment.Load();
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
