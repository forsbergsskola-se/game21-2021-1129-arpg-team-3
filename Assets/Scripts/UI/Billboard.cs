using UnityEngine;

public class Billboard : MonoBehaviour
{
    
    // For 2D sprites to face the camera.
    
    public Camera _camera;
    private void LateUpdate()
    {
        transform.forward = _camera.transform.forward;
    }
}
