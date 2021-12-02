using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
	public Transform miniMapCameraTransform;
	public Transform playerTransform;
	private Vector3 cameraFromPlayerOffset;
	
	void Start()
	{
		cameraFromPlayerOffset = new Vector3(0, 11, 0);
	}
	private void LateUpdate() {
		miniMapCameraTransform.position = playerTransform.position + cameraFromPlayerOffset;
	}
}