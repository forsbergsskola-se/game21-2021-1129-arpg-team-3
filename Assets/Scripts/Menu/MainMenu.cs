using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public InventoryObjects inventory;
	public void NewGame() 
	{
		Time.timeScale = 1f;
		FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
		SceneManager.LoadScene(1);
	}
	public void LoadGame()
	{
		SceneManager.LoadScene(1);
		inventory.Load();
	}
	public void ExitGame()
	{
		FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
		Debug.Log("I'm out, beaches!");
		Application.Quit();
	}
}
