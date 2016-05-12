using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour {

	public GameObject panelImg, StartButton, CreditsButton, OptionsButton, QuitButton, BackToHome, BackToOptions, OptionsMenuParent, SoundMenuParent, DisplayMenuParent, StartText, StartButImg, CreditsObject, CreditsPane, OptionsMenuHomeButton, CreditsMenuHomeButton;
	public GameObject LobbyCanvas;
	public Image endImg;
	public float CameraSpeed = 10f;
	public static bool OnCredits = false;
	public GameObject Wisp;
	float currentLerpTime, lerpTime;
	Vector3 startPos, endPos;
	bool lerp;
	TextMesh OptionsMenuText;


	void Start()
	{
		startPos = Camera.main.transform.position;
		endPos = Camera.main.transform.position;
		lerp = false;
		lerpTime = 1f;
		currentLerpTime = 0;
		OptionsMenuText = OptionsMenuHomeButton.GetComponent<TextMesh> ();

	}

	void Update()
	{
		if (lerp) 
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			float perc = currentLerpTime / lerpTime;
			Camera.main.transform.position = Vector3.Lerp (startPos, endPos, perc);
			if (Camera.main.transform.position == endPos) {
				lerp = false;
				currentLerpTime = 0;
			}
		}
	}

	public void LoadMenu()
	{
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);

		startPos = Camera.main.transform.position;
		endPos = new Vector3 (4.59f, 0.52f, 106.24f);
		lerp = true;
		StartCoroutine (toClearLerp ());

		StartButton.SetActive(true);
		CreditsButton.SetActive(true);
		OptionsButton.SetActive(true);
		QuitButton.SetActive(true);
		StartText.SetActive (false);
		WispController.speed = 0.009f;

	}

	public void StartGame()
    {
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);
		AkSoundEngine.PostEvent ("Stop_Menu_Music", gameObject);
		AkSoundEngine.StopAll ();
		AkBankManager.UnloadBank ("UIbank.bk");

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
        Application.LoadLevel("buildprototype");
    }

	public void EnableMatchmaking(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		LobbyCanvas.SetActive (true);
	}

    public void QuitGame()
    {
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);

		startPos = Camera.main.transform.position;
		endPos = new Vector3 (3.24f, 48.96f, 53.54f);
		lerp = true;
		StartCoroutine (colorLerp ());
        
    }

    public void CreditsPage()
    {
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);

		startPos = Camera.main.transform.position;
		endPos = new Vector3 (18.24f, 0.52f, 101.77f);
		lerp = true;
		CreditsPane.SetActive (true);
		CreditsObject.SetActive (true);
		OnCredits = true;
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        OptionsButton.SetActive(false);
        QuitButton.SetActive(false);
		CreditsMenuHomeButton.SetActive (true);
		WispController.speed = 0.009f;

    }
    
    public void OptionsPage()
    {
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);

		startPos = Camera.main.transform.position;
		endPos = new Vector3 (-10.54f, 0.52f, 114.351f);
		lerp = true;
		StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        OptionsButton.SetActive(false);
        QuitButton.SetActive(false);
        BackToHome.SetActive(true);
        OptionsMenuParent.SetActive(true);
		WispController.speed = 0.009f;

    }

    public void BackToHomeMenu()
    {
		//Audio Call
		AkSoundEngine.PostEvent ("Play_UI_Back", gameObject);

		startPos = Camera.main.transform.position;
		endPos = new Vector3 (4.59f, 0.52f, 106.24f);
		lerp = true;
		StartButton.SetActive(true);
        CreditsButton.SetActive(true);
        OptionsButton.SetActive(true);
        QuitButton.SetActive(true);
        BackToHome.SetActive(false);
        OptionsMenuParent.SetActive(false);
		SoundMenuParent.SetActive(false);
		DisplayMenuParent.SetActive (false);
		WispController.speed = 0.009f;     
    }

	public void BackToHomeMenuFromCredits()
	{
		//Audio Call
		AkSoundEngine.PostEvent ("Play_UI_Back", gameObject);

		startPos = Camera.main.transform.position;
		endPos = new Vector3 (4.59f, 0.52f, 106.24f);
		lerp = true;
		StartButton.SetActive(true);
		CreditsButton.SetActive(true);
		OptionsButton.SetActive(true);
		QuitButton.SetActive(true);
		CreditsMenuHomeButton.SetActive (false);
		if (OnCredits) 
		{
			OnCredits = false;
			CreditsObject.GetComponent<Credits>().BackToHomeAfterCredits();
		}
	}

    public void BackToOptionsMenu()
    {
		//Audio Call
		AkSoundEngine.PostEvent ("Play_UI_Back", gameObject);

        BackToHome.SetActive(true);
        BackToOptions.SetActive(false);
        OptionsMenuParent.SetActive(true);
        SoundMenuParent.SetActive(false);
        DisplayMenuParent.SetActive(false);
		WispController.speed = 0.009f;
    }

    public void SoundMenu()
    {
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);

		BackToHome.SetActive(true);
		DisplayMenuParent.SetActive (false);
        SoundMenuParent.SetActive(true);
		WispController.speed = 0.009f;
    }

    public void DisplayMenu()
    {
		//Audio call
		AkSoundEngine.PostEvent ("Play_UI_Select", gameObject);

		BackToHome.SetActive(true);
		SoundMenuParent.SetActive(false);
		DisplayMenuParent.SetActive(true);
		WispController.speed = 0.009f;
    }

	IEnumerator colorLerp(){
		float ElapsedTime = 0.0f;
		float TotalTime = 1.5f;
		while (ElapsedTime < TotalTime)
		{
			ElapsedTime += Time.deltaTime;
			endImg.color = Color.Lerp(Color.clear, Color.black, (ElapsedTime / TotalTime));
			yield return null;
		}
		yield return new WaitForSeconds (3);
		Application.Quit();
	}

	IEnumerator toClearLerp(){
		float ElapsedTime = 0.0f;
		float TotalTime = 3f;
		Image startImg;
		while(ElapsedTime < TotalTime){
			ElapsedTime += Time.deltaTime;
			startImg = panelImg.GetComponent<Image>();
			startImg.color = Color.Lerp(startImg.color, Color.clear, (ElapsedTime / TotalTime));
			yield return null;
		}
		yield return new WaitForSeconds (2);

	}
	
}
