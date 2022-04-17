using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
    public GameObject hinge;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
	    transform.RotateAround(hinge.transform.position, Vector3.up, 90);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Terrain/OpenDoorLarge");

        // var targetRotation = Quaternion.LookRotation(hinge.transform.position - transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, 2f * Time.deltaTime);
        // gameObject.SetActive(false);
    }
}
