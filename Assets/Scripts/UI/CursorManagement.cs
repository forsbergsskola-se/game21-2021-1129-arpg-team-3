using UnityEngine;

public class CursorManagement : MonoBehaviour 
{
	[SerializeField] private GameObject rallypoint;
	private GameObject rally;
	public Texture2D invalid, attack, arrowCursor, crosshairs, handOpen, talk, doorUnlocked, doorLocked;

	// public Texture2D cursorIdle;
	// public Texture2D cursorEnemy;
	// public Texture2D cursorNegative;
	
	public void CursorChange(int tag) {
		switch (tag) {
			case 1: //Default
				Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.Auto);
				break;
			case 2: //Ground
				Cursor.SetCursor(crosshairs, new Vector2(crosshairs.width * 0.5f, crosshairs.height * 0.5f), CursorMode.Auto);
				break;
			case 3: //Enemy
				Cursor.SetCursor(attack, new Vector2(attack.width * 0.5f, attack.height * 0.5f), CursorMode.Auto);
				break;
			case 4: //Grabbing
				Cursor.SetCursor(handOpen, new Vector2(handOpen.width * 0.5f, handOpen.height * 0.5f), CursorMode.Auto);
				break;
			case 5: //Unlocked Door
				Cursor.SetCursor(doorUnlocked, new Vector2(doorUnlocked.width * 0.5f, doorUnlocked.height * 0.5f), CursorMode.Auto);
				break;
			case 6: //Locked door
				Cursor.SetCursor(doorLocked, new Vector2(doorLocked.width  * 0.5f, doorLocked.height * 0.5f), CursorMode.Auto);
				break;
			case 7: //Talk
				Cursor.SetCursor(talk, new Vector2(talk.width * 0.5f, talk.height  * 0.5f), CursorMode.Auto);
				break;
			case 8: //Invalid
				Cursor.SetCursor(invalid, new Vector2(invalid.width * 0.5f, invalid.height * 0.5f), CursorMode.Auto);
				break;
		}
	}
	public void SpawnRallyPoint(Vector3 position) {
		rally = Instantiate(rallypoint, new Vector3(position.x, position.y, position.z), transform.rotation);
	}
	public void DeSpawnRallyPoint() {
		Destroy(rally);
	}
}
