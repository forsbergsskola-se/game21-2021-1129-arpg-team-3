using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public InventoryObjects inventory;
    public InventoryObjects equipment;
    public GameObject pauseMenuUI;
    public PlayerController player;
    public InventoryHider inventoryHider;
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            {
                if (player.inDialogue) 
                {
                    Resume();
                }
                else 
                {
                    Pause();
                }
            }
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
        inventoryHider.GetComponent<Canvas>().enabled = false;
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

    public void LoadMainMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        Debug.Log("Load me the menu now!");
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
        Debug.Log("Quitting diz shiet");
        Application.Quit();
    }
}
