using UnityEngine;

public class RallyPoint : MonoBehaviour
{
	
	// Simple method to despawn the rallypoint when the player reaches its location.
	
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			Destroy(this.gameObject);
		}
	}
}
