using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	// Controls the main menu at the start of the game

	public Texture2D arrowCursor;
	public InventoryObjects inventory;
	public GameObject image;
	public GameObject controls;
	public GameObject credits;
	public GameObject splash;
	private bool canShow = true;
	private void Awake() 
	{
		Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.Auto);
	}
	private void Update()
	{
		// Shows and hides graffiti
		if (canShow) 
		{
			StartCoroutine(Flash());
			StartCoroutine(Wait());
		}
	}
	private IEnumerator Flash()
	{ 
		image.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		image.SetActive(true);
	}
	private IEnumerator Wait()
	{
		canShow = false;
		yield return new WaitForSeconds(10f);
		canShow = true;
	}
	public void NewGame() 
	{
		// Unpauses game. Important!
		Time.timeScale = 1f;
		FMODUnity.RuntimeManager.PlayOneShot("event:/Clicks/UiClick");
		SceneManager.LoadScene(1);
	}
	// Unused.
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
	// Shows Controls and Credits screens
	public void ControlsOn()
	{
		controls.SetActive(true);
	}
	public void ControlsOff()
	{
		controls.SetActive(false);
	}
	public void CreditsOn()
	{
		credits.SetActive(true);
	}
	public void CreditsOff()
	{
		credits.SetActive(false);
	}
	// Closes logo splash screen
	public void Close()
	{
		splash.SetActive(false);
	}
}
