using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/MessageSO", fileName = "MessageSO")]

public class MessageSO : ScriptableObject {
	[SerializeField] private string mText;
	public string MText => mText;
}
