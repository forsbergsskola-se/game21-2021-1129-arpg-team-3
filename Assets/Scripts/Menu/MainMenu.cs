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
	public void Close()
	{
		splash.SetActive(false);
	}
}
