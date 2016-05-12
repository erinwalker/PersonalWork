using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WispController : MonoBehaviour {
	private Vector3 padDirection;
	GameObject myEventSystem;
	public static float speed = 0.009f;
	public static bool onScollbar;
    GameObject collisionObject;
	public float SpeedTemp;
	public Vector2 Joystick;

	// Use this for initialization
	void Start() 
	{
		//Loads UI Sounds
		AkBankManager.LoadBank ("UIbank.bk");
		AkSoundEngine.PostEvent ("Play_NEW_MENU_MUSIC", gameObject);

		myEventSystem = GameObject.Find("EventSystem");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		onScollbar = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		SpeedTemp = speed;
		padDirection.z = Input.GetAxis ("Horizontal");
		padDirection.x = Input.GetAxis ("Vertical");
		Joystick = new Vector2 (padDirection.z, padDirection.x);

        try
        {
            if (onScollbar)
            {
				//If you have already selected a scrollbar to use and you are done changing it, press the submit button again to release the wisp.
                if (myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject.tag == "Scroll" && Input.GetButtonDown("Submit"))
                {
                    myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
                    onScollbar = false;
					speed = 0.009f;
					Input.ResetInputAxes();
                }
            }
        }
        catch (Exception e) {}

		if(!onScollbar)
		{

			try
			{
				//If the wisp is on a scrollbar and you want to use it press submit button and it will lock movement of wisp and allow user to control scrollbar
	            if (collisionObject.tag == "Scroll" && Input.GetButtonDown("Submit"))
	            {
	                myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(collisionObject);
	                onScollbar = true;
	            }
			}
			catch(Exception e){}
		}
	}

	void FixedUpdate()
	{
		if (!onScollbar) {
			//Movement of wisp
			this.transform.position += new Vector3 (padDirection.z * speed, padDirection.x * speed, 0);
			
			//Lock the wisp to the screen
			Vector3 _viewPos = Camera.main.WorldToViewportPoint (this.transform.position);
			_viewPos.x = Mathf.Clamp01 (_viewPos.x);
			_viewPos.y = Mathf.Clamp01 (_viewPos.y);
			this.transform.position = Camera.main.ViewportToWorldPoint (_viewPos);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		AkSoundEngine.PostEvent ("Play_Menu_Toggle", gameObject);

		//Create friction when on a button or scrollbar
		if (col.tag == "MenuButton") {
			speed = 0.006f;
			myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(col.gameObject);
		}
		if (col.tag == "Scroll") {
			speed = 0.006f;
		}
        collisionObject = col.gameObject;
	}

	//Reset speed and deselect object
	void OnTriggerExit(Collider other)
	{
		speed = 0.009f;
		myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
		onScollbar = false;
	}
}
