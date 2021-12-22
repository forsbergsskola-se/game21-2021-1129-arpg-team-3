using UnityEngine;

public class Projectile : MonoBehaviour {
	
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float projectileLifespan;
	private Rigidbody rigidbody;
	
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		rigidbody.AddForce(rigidbody.transform.forward * projectileSpeed * Time.deltaTime);
		Destroy(gameObject, projectileLifespan);
	}
}
