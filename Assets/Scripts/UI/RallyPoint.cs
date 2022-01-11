using UnityEngine;

public class RallyPoint : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			Destroy(this.gameObject);
		}
	}
}
