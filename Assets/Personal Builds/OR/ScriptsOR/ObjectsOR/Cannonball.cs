
using UnityEngine;

public class Cannonball : MonoBehaviour {
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
