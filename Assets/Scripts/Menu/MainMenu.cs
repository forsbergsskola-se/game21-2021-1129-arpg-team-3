using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public Texture2D arrowCursor;
	public InventoryObjects inventory;
	public GameObject image;
	private bool canShow = true;
	private void Awake() 
	{
		Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.Auto);
	}
	private void Update() {
		if (canShow) {
			Show();
		}
	}

	private void Show() {
		StartCoroutine(Flash());
	} 
	
	private IEnumerator Flash()
	{
		image.SetActive(true);
		yield return new WaitForSeconds(10f);
		image.SetActive(false);
	}
	
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
