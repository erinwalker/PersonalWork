using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class LanguageSelector : MonoBehaviour {

    public DialogueDatabase English;
    public DialogueDatabase Spanish;

	void Awake()
	{
		if (LanguageSelectorMenu.Language == "English") {
			SelectEnglish ();
		} else if (LanguageSelectorMenu.Language == "Spanish") {
			SelectSpanish();
		}
	}

    public void SelectEnglish()
    {
        Debug.Log("Removing" + Spanish.name);
        DialogueManager.RemoveDatabase(Spanish);
        Debug.Log("Adding" + English.name);
        DialogueManager.AddDatabase(English);
        PersistentDataManager.Apply();

    }

    public void SelectSpanish()
    {
        Debug.Log("Removing" + English.name);
        DialogueManager.RemoveDatabase(English);
        Debug.Log("Adding" + Spanish.name);
        DialogueManager.AddDatabase(Spanish);
        PersistentDataManager.Apply();
    }


}
