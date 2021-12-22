using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _camera;
    private void LateUpdate()
    {
        transform.forward = _camera.transform.forward;
    }
}
