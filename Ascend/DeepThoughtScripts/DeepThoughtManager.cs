using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

//public enum TypeOfDT { Dialogue, Trigger, PoI}

public class DeepThoughtManager : MonoBehaviour
{
    //Public Lists
    public List<DeepThought> DeepThoughtList;
	private List<DeepThought> tempDeepThoughtList;

    //Deep Thought Sub Class temporary instances
    [HideInInspector]
    public DeepThoughtDialogue tempDeepThoughtDia;
    [HideInInspector]
    public DeepThoughtTrigger tempDeepThoughtTrig;
    [HideInInspector]
    public PointofInterest tempPOI;


    //Public Variables
    public GameObject Canvas;
    public Text DTTextObject;
    public int FadeDuration;
    public float MinimumSpawnDistanceBetweenText;
    [HideInInspector]
    public Vector2 LastTextLocation;
    [HideInInspector]
    public float DistanceBetweenText;
    [HideInInspector]
    public Button Button;

    //Constants
    public const float MAX_VALUE = 15;
    const int MAX_FONT_SIZE = 30;
    const int MIN_FONT_SIZE = 15;
    const float MAX_DURATION = 5;

    //Private Variables
    private int indexOfList;
    private NPCMovement npcMovement;
    private bool displayingDT;
    public bool canDisplayText;
    private int lastIndex;
    private bool displayReactableDT;
	private bool inConversation;
    private string tempConvo;
    private int dtCounter;

	// Use this for initialization
	void Start () 
	{
        Lua.RegisterFunction("AddDeepThoughtDialogue", this, typeof(DeepThoughtManager).GetMethod("AddDeepThoughtDialogue"));
        indexOfList = 0;
        npcMovement = GameObject.Find("NPC").GetComponent<NPCMovement>();
        displayingDT = false;
        canDisplayText = false;
        LastTextLocation = new Vector2(0, 0);
        lastIndex = 0;
        displayReactableDT = false;
        DeepThoughtList = new List<DeepThought>();
		inConversation = ConversationTracker.InConversation;
        dtCounter = 0;
	} 

	// Update is called once per frame
	void Update () 
	{
        UpdateDeepThoughtPoI();
		for(indexOfList = 0; indexOfList < DeepThoughtList.Count; indexOfList++)
			//Debug.Log("Deep Thought: " + DeepThoughtList[indexOfList].Text);
        if (canDisplayText)
        {
            if (DeepThoughtList.Count > 0)
            {
                if (displayingDT == false)
                    StartCoroutine(Timer(DisplayDeepThoughts()));

            }
        }
		//FROM^ART changed the input key
        //if(Input.GetKeyDown(KeyCode.I))
        //{
           //canDisplayText = !canDisplayText;
        //}
		canDisplayText = ParserFunctionsScript.InPerspectiveShift;

        if (canDisplayText == false)
            lastIndex = 0;

        //Enter conversation when a reactable thought is active
        if (displayReactableDT && inConversation == false)
        {
            //Change the key to an Xbox button
            if (Input.GetKeyDown(KeyCode.O))
            {
                ReactToDeepThought(tempConvo);
            }
        }
	}

    void OnConversationStart(Transform actor)
    {
        ReduceDeepThoughtDialogueWeight(1);
		inConversation = ConversationTracker.InConversation;
		/*tempDeepThoughtList = new List<DeepThought> ();
        foreach(DeepThought DT in DeepThoughtList)
        {
            if(DT.IsReactableThought == true)
            {
				tempDeepThoughtList.Add(DT);
            }
        }
		foreach (DeepThought tempDT in tempDeepThoughtList) 
		{
			foreach(DeepThought DT in DeepThoughtList)
			{
				if(tempDT == DT)
				{
					DeepThoughtList.Remove(DT);
				}
			}
		}*/
    }
    void OnConversationEnd(Transform actor)
    {
		/*foreach (DeepThought tempDT in tempDeepThoughtList) 
		{
			DeepThoughtList.Add(tempDT);
		}*/
        inConversation = ConversationTracker.InConversation;
        ReduceDeepThoughtDialogueWeight(1);
    }

    //Dialogue nodes call this function from their script line
    //Calls the DeepThoughtDialogue SetValues function to set the initial values of the DeepThought 
    public void AddDeepThoughtDialogue()
    {
        tempDeepThoughtDia = this.GetComponent<DeepThoughtDialogue>();
        tempDeepThoughtDia.SetValues();
        DeepThoughtList.Add(tempDeepThoughtDia);
    }

    //Called when a conversation ends and decreases dialogue type deep thoughts values
    //If the deep thought value is less than or equal to zero it removes the deep thought from all list
    void ReduceDeepThoughtDialogueWeight(float _weight)
    {
        if (DeepThoughtList.Count > 0)
        {
            for (indexOfList = 0; indexOfList < DeepThoughtList.Count; indexOfList++)
            {
                if (DeepThoughtList[indexOfList].TypeOfDeepThought == TypeOfDT.Dialogue)
                {
                    DeepThoughtList[indexOfList].Value -= _weight;

                    if (DeepThoughtList[indexOfList].Value <= 0)
                    {
                        DeepThoughtList.RemoveAt(indexOfList);
                    }
                }
            }
        }
    }

    //Adds a deep thought associated to a trigger
    //Called from the DeepThoughtTrigger script when you enter a trigger
    public void AddDeepThoughtTrigger()
    {
        DeepThoughtList.Add(tempDeepThoughtTrig);
    }

    //Removes deep thought of trigger type when you exit a trigger
    public void RemoveDeepThoughtTrigger(DeepThought _deepThought)
    {
        if (DeepThoughtList.Count > 0)
        {
            for (indexOfList = 0; indexOfList < DeepThoughtList.Count; indexOfList++)
            {
                if (DeepThoughtList[indexOfList].TypeOfDeepThought == TypeOfDT.Trigger)
                {
                    if (DeepThoughtList[indexOfList].Text == _deepThought.Text)
                    {
                        DeepThoughtList.RemoveAt(indexOfList);
                    }
                }
            }
        }
    }

    //Adds deep thoughts from points of interest based on distance from player
    void UpdateDeepThoughtPoI()
    {
        for (indexOfList = 0; indexOfList < DeepThoughtList.Count; indexOfList++)
        {
            if (DeepThoughtList[indexOfList].TypeOfDeepThought == TypeOfDT.PoI)
            {
                //Delete deep thought if out of max or min range
                if (DeepThoughtList[indexOfList].Value >= MAX_VALUE - 1 || DeepThoughtList[indexOfList].Value <= 0)
                {
                    DeepThoughtList.RemoveAt(indexOfList);
                }
            }
        }
    }

    //Handles displaying the deep thoughts text on screen
    int DisplayDeepThoughts()
    {

        Component newText = Instantiate(DTTextObject) as Component;
        RectTransform _rectTrans = newText.gameObject.GetComponent<RectTransform>();
        Text _textObj = newText.gameObject.GetComponent<Text>();
        newText.transform.SetParent(Canvas.transform);

        if(dtCounter >= DeepThoughtList.Count)
        {
            dtCounter = 0;
            lastIndex = 0;
        }

        if (DeepThoughtList.Count > 0)
        {
            for(indexOfList = lastIndex; indexOfList < DeepThoughtList.Count; indexOfList++)
            {
                if(DeepThoughtList[indexOfList].IsReactableThought)
                {
                    //Set Position
                    _rectTrans.localPosition = ChoosePosition(DeepThoughtList[indexOfList].Value);

                    //Set Font Size
                    _textObj.fontSize = Mathf.FloorToInt(MAX_FONT_SIZE);

                    _textObj.text = DeepThoughtList[indexOfList].Text;
                    _textObj.color = Color.yellow;

                    newText.gameObject.SetActive(true);
                    GameObject.FindGameObjectWithTag("DTText").GetComponent<CanvasRenderer>().SetAlpha(0.0f);
                    GameObject.FindGameObjectWithTag("DTText").GetComponent<Text>().CrossFadeAlpha(1.0f, FadeDuration, false);
					Debug.Log( GameObject.FindGameObjectWithTag("DTText"));
                    lastIndex = indexOfList + 1;
                    displayReactableDT = true;
                    dtCounter++;
                    return indexOfList;
                }
            }

            int _randNum;
            do
            {
                _randNum = Random.Range(0, DeepThoughtList.Count);
            }
            while (DeepThoughtList[_randNum].IsReactableThought);

            //Set Position
            _rectTrans.localPosition = ChoosePosition(DeepThoughtList[_randNum].Value);

            //Set Font Size
            _textObj.fontSize = Mathf.FloorToInt(MAX_FONT_SIZE * (DeepThoughtList[_randNum].Value / MAX_VALUE));
            if (DeepThoughtList[_randNum].Value < 10)
                _textObj.fontSize = MIN_FONT_SIZE;

            _textObj.text = DeepThoughtList[_randNum].Text;

            newText.gameObject.SetActive(true);

            GameObject.FindGameObjectWithTag("DTText").GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            GameObject.FindGameObjectWithTag("DTText").GetComponent<Text>().CrossFadeAlpha(1.0f, FadeDuration, false);
			Debug.Log( GameObject.FindGameObjectWithTag("DTText"));
            dtCounter++;
            return _randNum;
        }
        return 0;
    }

    //Split the screen into three sections the higher the weight the closer to the middle it spawns
    //I randomly choose a number between 1 to 4 to decide which side of the screen is displays on (top, bottom, left right)
    //-----------------
    //|               |
    //|  -----------  |
    //|  |  -----  |  |
    //|  |  |   |  |  |
    //|  |  -----  |  |
    //|  -----------  |
    //|               |
    //-----------------
    Vector3 ChoosePosition(float _weight)
    {
        int _randNum = Random.Range(1, 5);
        int _count = 0;
        Vector3 vector = new Vector3();
        do{
            if (_weight <= MAX_VALUE && _weight > MAX_VALUE*(2/3))
            {
                vector = new Vector3(Random.Range(-164, 164), Random.Range(-78, 78), 0);
            }
            if (_weight <= MAX_VALUE * (2 / 3) && _weight > MAX_VALUE * (1 / 3))
            {
                switch (_randNum)
                {
                    case 1:
                        vector = new Vector3(Random.Range(-327, -164), Random.Range(-154, -78), 0);
                        break;
                    case 2:
                        vector = new Vector3(Random.Range(-327, -164), Random.Range(78, 154), 0);
                        break;
                    case 3:
                        vector = new Vector3(Random.Range(164, 327), Random.Range(-154, -78), 0);
                        break;
                    case 4:
                        vector = new Vector3(Random.Range(164, 327), Random.Range(78, 154), 0);
                        break;
                }
            }
            if (_weight <= MAX_VALUE * (1 / 3))
            {
                switch (_randNum)
                {
                    case 1:
                        vector = new Vector3(Random.Range(-490, -327), Random.Range(-230, -154), 0);
                        break;
                    case 2:
                        vector = new Vector3(Random.Range(-490, -327), Random.Range(154, 230), 0);
                        break;
                    case 3:
                        vector = new Vector3(Random.Range(327, 490), Random.Range(154, 230), 0);
                        break;
                    case 4:
                        vector = new Vector3(Random.Range(327, 490), Random.Range(-230, -154), 0);
                        break;
                }
            }
            DistanceBetweenText = Vector3.Distance(LastTextLocation, vector);
            _count++;
        }
        while(Vector3.Distance(LastTextLocation, vector) <= MinimumSpawnDistanceBetweenText && _count < 10);
        LastTextLocation = vector;
        return vector;
    }

    IEnumerator Timer(int index)
    {
        if (displayReactableDT)
        {
            displayingDT = true;

            if (inConversation == false)
            {
                /*Component _gButton = Instantiate(Button) as Component;
                Button _bButton = _gButton.GetComponent<Button>();
                RectTransform _rectTrans = _gButton.gameObject.GetComponent<RectTransform>();
                _gButton.transform.SetParent(Canvas.transform);
                _rectTrans.localPosition = new Vector3(0, 0, 0);
                _bButton.onClick.AddListener(delegate() { ReactButton(DeepThoughtList[index].ReactableConvo); });*/
                tempConvo = DeepThoughtList[index].ReactableConvo;
            }

            yield return new WaitForSeconds(MAX_DURATION);
			
            GameObject.FindGameObjectWithTag("DTText").GetComponent<Text>().CrossFadeAlpha(0.0f, FadeDuration, false);

            yield return new WaitForSeconds(FadeDuration);

            //Destroy(GameObject.FindGameObjectWithTag("ReactButton"));
            Destroy(GameObject.FindGameObjectWithTag("DTText"));
            displayingDT = false;
            displayReactableDT = false;
        }
        else
        {
            displayingDT = true;
            yield return new WaitForSeconds(MAX_DURATION * (DeepThoughtList[index].Value / MAX_VALUE));
            GameObject.FindGameObjectWithTag("DTText").GetComponent<Text>().CrossFadeAlpha(0.0f, FadeDuration, false);
            yield return new WaitForSeconds(FadeDuration);
            Destroy(GameObject.FindGameObjectWithTag("DTText"));
            displayingDT = false;
        }
    }

    /*public void ReactButton(string convo)
    {
        canDisplayText = false;
        DialogueManager.StartConversation(convo);
    }*/

    void ReactToDeepThought(string convo)
    {
        canDisplayText = false;
        DialogueManager.StartConversation(convo);
    }
}