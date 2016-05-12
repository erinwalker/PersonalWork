using UnityEngine;
using System.Collections;
using System;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class UnityDialogueUIOverride : UnityUIDialogueUI {

	public Response[] CurrentResponses;
	public Text NPCSubtitle;

	public override void ShowResponses (Subtitle subtitle, Response[] responses, float timeout)
	{
		CurrentResponses = responses;
		foreach (var res in responses) 
		{
			Debug.Log(res.formattedText.text);
		}
	}

	void Update()
	{
		try
		{
			if (NPCSubtitle.text == "(NPC Subtitle)") 
			{
				NPCSubtitle.text = "";
			}
		}
		catch(SystemException e)
		{
		}

	}

	public void OnMobileResponseSelection(int index)
	{
		OnClick (CurrentResponses [index]);
		Debug.Log(index);
	}
}
