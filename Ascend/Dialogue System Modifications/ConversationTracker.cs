using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ConversationTracker : MonoBehaviour {

	public static bool InConversation;

	void OnConverastionStart(Transform actor)
	{
		InConversation = true;
	}

	void OnConverastionEnd(Transform actor)
	{
		InConversation = false;
	}

}
