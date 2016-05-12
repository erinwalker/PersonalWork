using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class StoppedDialogue : MonoBehaviour {

    List<int> DroppedConversationsIDs = new List<int>();
    List<int> DroppedDialogueIDs = new List<int>();

    DialogueEntry dialogueEntry, continueEntry;
    int conversationID;
    int dialogueEntryID;

	public float SecondsBeforeWarning = DialogueLua.GetVariable("SecondsBeforeWarning").AsFloat;
	public float SecondsBeforeConversationEnd = DialogueLua.GetVariable("SecondsBeforeConversationEnd").AsFloat;

	
	// Update is called once per frame
	void Update () 
    {
	    if(DroppedConversationsIDs.Count > 0)
        {
            DialogueLua.SetVariable("CanContinue", true);
        }
        else
        {
            DialogueLua.SetVariable("CanContinue", false);
        }
	}

    void OnConversationLine(Subtitle subtitle)
    {
		SecondsBeforeWarning = DialogueLua.GetVariable("SecondsBeforeWarning").AsFloat;
		SecondsBeforeConversationEnd = DialogueLua.GetVariable("SecondsBeforeConversationEnd").AsFloat;
        if (DialogueLua.GetVariable("IsTimeSensitive").AsBool == true)
        {
            StopCoroutine("Timer");
            StartCoroutine("Timer");
        }
        else
        {
            StopCoroutine("Timer");
        }

		int count = 0;
        if (DroppedConversationsIDs.Count > 0)
        {
            if (DroppedConversationsIDs.Contains(subtitle.dialogueEntry.conversationID))
            {
                foreach (var dialogue in DroppedDialogueIDs)
                {
                    if (subtitle.dialogueEntry.id == dialogue)
                    {
						var lastConversation = DialogueManager.MasterDatabase.GetConversation(DialogueManager.LastConversationStarted);
						foreach (var entry in lastConversation.dialogueEntries) {
							if (string.Equals(entry.DialogueText, "Continue Conversations")) {
								continueEntry = entry;
								break;
							}
						}
						Debug.Log(subtitle.dialogueEntry.id);
						continueEntry.outgoingLinks.Clear();
						DroppedDialogueIDs.RemoveAt(count);
						DroppedConversationsIDs.RemoveAt(count);
						break;
                    }
					count++;
                }
            } 
        }

		if (DroppedConversationsIDs.Count > 0) {
			for (count = 0; count < DroppedConversationsIDs.Count; count++) {
				Debug.Log ("ConvoID: " + DroppedConversationsIDs [count] + " DialogueID: " + DroppedDialogueIDs [count] + " Index: " + count);
			}
		}
    }

     public void OnConversationStart(Transform actor) {
        var conversation = DialogueManager.MasterDatabase.GetConversation(DialogueManager.LastConversationStarted);
        var wasInterrupted = (DroppedConversationsIDs.Contains(conversation.id));
        DialogueLua.SetVariable("CanContinue", wasInterrupted);
        if (wasInterrupted) {
            continueEntry = null;
            foreach (var entry in conversation.dialogueEntries) {
                if (string.Equals(entry.DialogueText, "Continue Conversations")) {
                    continueEntry = entry;
                    break;
                }
            }
           continueEntry.outgoingLinks.Clear();
            foreach (int entryID in DroppedDialogueIDs) {
                continueEntry.outgoingLinks.Add(new Link(conversation.id, continueEntry.id, conversation.id, entryID));
            }
        }
    }

    void OnConversationTimeout()
    {
        AddConversationToList();
        DialogueManager.StopConversation();
    }

    void OnConversationCancelled(Transform actor)
    {
        AddConversationToList();
        DialogueManager.StopConversation();
    }

    public void AddConversationToList()
    {
        dialogueEntry = DialogueManager.CurrentConversationState.subtitle.dialogueEntry;
        conversationID = dialogueEntry.conversationID;
        dialogueEntryID = dialogueEntry.id;

        DroppedConversationsIDs.Add(conversationID);
        DroppedDialogueIDs.Add(dialogueEntryID);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(SecondsBeforeWarning);
        Debug.Log("Warning");
        yield return new WaitForSeconds(SecondsBeforeConversationEnd);
		AddConversationToList();
        DialogueManager.StopConversation();
    }
}