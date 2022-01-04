using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float _elapsedTime;

    private void Start()
    {
        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
      
        if(lifeTime < _elapsedTime) Destroy(gameObject);
    }
}
