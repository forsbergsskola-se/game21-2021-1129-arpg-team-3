using UnityEngine;

public class MiniMap : MonoBehaviour
{
	public Transform miniMapCameraTransform;
	public Transform playerTransform;
	private Vector3 cameraFromPlayerOffset;
	[SerializeField] float rotationSpeed = 120f;
	[SerializeField] Transform player;
	[SerializeField] private Camera cam;
	private Vector3 previousPosition;
	
	void Start()
	{
		cameraFromPlayerOffset = new Vector3(0, 11, 0);
	}
	private void LateUpdate() 
	{
		miniMapCameraTransform.position = playerTransform.position + cameraFromPlayerOffset;
		var position = player.position;
		transform.RotateAround(position, player.up, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed); 
		
		if (Input.GetMouseButton(2)) 
		{
			Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
			cam.transform.position = player.position;
			cam.transform.Rotate(new Vector3(0,0.1f,0),-direction.x*90,Space.World);
			cam.transform.Translate(new Vector3(0,0,0));
			previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);        
		}
	}
}
