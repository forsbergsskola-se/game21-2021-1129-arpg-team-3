using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public InventoryObjects inventory;
    public InventoryObjects equipment;
    
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
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