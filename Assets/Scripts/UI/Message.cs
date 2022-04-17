using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour 
{
	
	// Input field for messages used for lore.
	
	// public MessageSO messageSO;
	[TextArea(15,20)]
	public string message;
	private void Awake() 
	{
		// message = messageSO.MText;
	}
}
