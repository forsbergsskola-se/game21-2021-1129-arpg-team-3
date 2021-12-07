using System;
using UnityEngine;

public class CursorManagement : MonoBehaviour {
	[SerializeField] private GameObject rallypoint;
	public static CursorManagement instance;
	private GameObject rally;
	public Texture2D invalid, attack, arrowCursor, crosshairs, handOpen, handClosed, doorUnlocked, doorLocked;

	// public Texture2D cursorIdle;
	// public Texture2D cursorEnemy;
	// public Texture2D cursorNegative;

	private void Awake() {
		instance = this;
	}

	public void CursorChange(int tag) {
		if (tag == 1) { //Default
			Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.Auto);
		}
		else if (tag == 2) { //Ground
			Cursor.SetCursor(crosshairs, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
		else if (tag == 3) { //Enemy
			Cursor.SetCursor(attack, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
		else if (tag == 4){ //Grabbing
			Cursor.SetCursor(handOpen, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
		else if (tag == 5) { //Unlocked Door
			Cursor.SetCursor(doorUnlocked, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
		else if (tag == 6) { //Locked door
			Cursor.SetCursor(doorLocked, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
		else if (tag == 7) { //Grabbed
			Cursor.SetCursor(handClosed, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
		else if (tag == 8) { //Invalid
			Cursor.SetCursor(invalid, new Vector2(crosshairs.width / 2, crosshairs.height / 2), CursorMode.Auto);
		}
	}
	public void SpawnRallyPoint(Vector3 position) {
		rally = Instantiate(rallypoint, new Vector3(position.x, 0.04f, position.z), transform.rotation);
	}
	public void DeSpawnRallyPoint() {
		Destroy(rally);
	}
}
