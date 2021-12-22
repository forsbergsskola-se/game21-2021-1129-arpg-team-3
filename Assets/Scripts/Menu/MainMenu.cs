using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public InventoryObjects inventory;
	public void NewGame() 
	{
		SceneManager.LoadScene(1);
	}
	public void LoadGame()
	{
		SceneManager.LoadScene(1);
		inventory.Load();
	}
	public void ExitGame()
	{
		Debug.Log("I'm out, beaches!");
		Application.Quit();
	}
}
