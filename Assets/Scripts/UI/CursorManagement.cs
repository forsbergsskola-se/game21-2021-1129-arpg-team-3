using System;
using UnityEngine;

public class CursorManagement : MonoBehaviour {
	[SerializeField] private GameObject rallypoint;
	private GameObject rally;
	public Texture2D cursorIdle;
	public Texture2D cursorEnemy;
	public Texture2D cursorNegative;

	private void Start() {
		Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
	}

	public void CursorChange(int tag) {
		if (tag == 1) {
			Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
		}
		else if (tag == 2) {
			Cursor.SetCursor(cursorEnemy, Vector2.zero, CursorMode.ForceSoftware);
		}
		else if (tag == 3){
			Cursor.SetCursor(cursorNegative, Vector2.zero, CursorMode.ForceSoftware);
		}
	}
	public void SpawnRallyPoint(Vector3 position) {
		rally = Instantiate(rallypoint, position, transform.rotation);
	}
	public void DeSpawnRallyPoint() {
		Destroy(rally);
	}
}
