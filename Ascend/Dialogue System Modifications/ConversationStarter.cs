using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ConversationStarter : MonoBehaviour {

	public void StartConvo(string convo)
	{
		DialogueManager.StartConversation (convo);
	}
}
