using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void Kill()
    {
       gameObject.SetActive(false);
       // Destroy(gameObject);
    }
}
