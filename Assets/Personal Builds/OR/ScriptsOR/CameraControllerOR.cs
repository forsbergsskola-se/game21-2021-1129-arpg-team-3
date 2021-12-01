using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerOR : MonoBehaviour {
	[SerializeField]Transform player;
	[SerializeField]float smoothTime = 0.3f;
	[SerializeField]float rotationSpeed = 120f;
	[SerializeField]float ZoomSpeed = 750f;
	[SerializeField]float MaxZoom = 10f;
	[SerializeField]float MinZoom = 2f;
	[SerializeField]float DefaultZoom = 5f;
	[SerializeField] private Camera cam;
	
	Vector3 offset;
	private Vector3 previousPosition;
	Vector3 _velocity = Vector3.zero;

	private void Start() {
		offset = transform.position - player.position;
		if (Camera.main is not null)
			Camera.main.orthographicSize = DefaultZoom;
		else {
			Debug.LogWarning("Main Camera is NULL!");
		}
	}
	private void LateUpdate() {
		//Smooth Chase Camera
		var position = player.position;
		var targetPosition = position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
		
		//Camera rotates horizontally with A & D keys
		transform.RotateAround(position, player.up, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed);
		
		//Camera zooms on scrollwheel
		if (Camera.main is not null) {
			Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomSpeed;

			if (Camera.main.orthographicSize > MaxZoom) {
				Camera.main.orthographicSize = MaxZoom;
			}
			if (Camera.main.orthographicSize < MinZoom) {
				Camera.main.orthographicSize = MinZoom;
			}
		}
		else {
			Debug.LogWarning("Main Camera is NULL!");
		}
		// Camera to rotate on click and drag
		if (Input.GetMouseButtonDown(2)) {
			previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
		}
		if (Input.GetMouseButton(2)) {
			Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
			cam.transform.position = player.position + offset;
			cam.transform.Rotate(new Vector3(0,-0.1f,0),-direction.x*90,Space.World);
			cam.transform.Translate(new Vector3(0,0,-10));
			previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);        
		}
	}
}
