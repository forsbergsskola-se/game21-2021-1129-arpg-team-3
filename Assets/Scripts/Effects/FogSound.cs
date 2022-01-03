using UnityEngine;

public class FogSound : MonoBehaviour
//Sound use only
{
    public GameObject fog;
    private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent;
    private void Start() 
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }
    private void Update() 
    {
        // Update player position on each frame
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        float distance = Vector3.Distance(transform.position, fog.transform.position);
        distance = 100 - distance/2;
        instance.setParameterByName("FoogAmount", distance);
    }
}
