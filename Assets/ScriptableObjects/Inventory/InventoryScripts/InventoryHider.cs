using UnityEngine;

public class InventoryHider : MonoBehaviour 
{
	private Canvas canvas;
	public PlayerController player;
	private void Start() 
	{
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
		TradeInterface.OnOpenInventory += OpenCloseRemote;
	}

	private void OpenCloseRemote()
	{
		if (!canvas.enabled)
		{
			canvas.enabled = true;
		}
		else
		{
			canvas.enabled = false;
		}
	}
	private void Update() 
	{
		if (Input.GetKeyUp(KeyCode.I) && !player.inDialogue) 
		{
			canvas.enabled = true;
			player.inDialogue = true;
			Time.timeScale = 0f;
		}
		else if (Input.GetKeyUp(KeyCode.I) && player.inDialogue)
		{
			canvas.enabled = false;
			player.inDialogue = false;
			Time.timeScale = 1f;
		}
	}
}
