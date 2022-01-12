using UnityEngine;

public class fire : MonoBehaviour

// Should rename to ObjectLifetime.

{
    [SerializeField] private float lifeTime;
    private float elapsedTime;
   
    private void Start()
    {
        elapsedTime = 0;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
      
        if(lifeTime < elapsedTime) Destroy(gameObject);
    }
}
