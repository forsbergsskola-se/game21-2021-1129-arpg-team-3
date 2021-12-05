using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
	public bool objectDestroyed = false;
    public void Kill()
    {
       gameObject.SetActive(false);
       objectDestroyed = true;
       // Destroy(gameObject);
    }
}
