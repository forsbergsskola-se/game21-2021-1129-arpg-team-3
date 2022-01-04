using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public InventoryObjects inventory;
    public InventoryObjects equipment;
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
        player.inDialogue = true;
    }
    public void Resume()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        player.inDialogue = false;
        spawnControl.Respawn();
    }
    
    public void LoadMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        Debug.Log("Load me the menu now!");
        //Can load Main Menu scene on click when we add Menu scene
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        Debug.Log("Quitting diz shiet");
        Application.Quit();
    }
}