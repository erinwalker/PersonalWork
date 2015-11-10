using UnityEngine;
using System.Collections;

public class ScriptEnabler : MonoBehaviour {
    //Make variables needed
    public GameObject player;
    BaseEnter baseEnter;
    ButtonRoom buttonRoom;
    Grid grid;
    KeyPickUp keyPickUp;
    Paint paint;
    public bool inFirstRoom;
    public bool inSecondRoom;
    public bool inThirdRoom;
    public bool inFourthRoom;

	// Use this for initialization
	void Start () 
    {
        //Gets scripts off player
        baseEnter = player.GetComponent<BaseEnter>();
        buttonRoom = player.GetComponent<ButtonRoom>();
        grid = player.GetComponent<Grid>();
        keyPickUp = player.GetComponent<KeyPickUp>();
        paint = player.GetComponent<Paint>();

        //Sets all scripts enabled to false initially
        baseEnter.enabled = false;
        buttonRoom.enabled = false;
        grid.enabled = false;
        keyPickUp.enabled = false;
        paint.enabled = false;

        //player starts in first room so it initially returns true for first room
        inFirstRoom = true;
        inSecondRoom = false;
        inThirdRoom = false;
        inFourthRoom = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Turns on and off scripts needed so they do not clash with each other
	    if(inFirstRoom == true)
        {
            keyPickUp.enabled = true;
            baseEnter.enabled = true;
        }
        else
        {
            keyPickUp.enabled = false;
            baseEnter.enabled = false;
        }

        if(inSecondRoom == true)
        {
            grid.enabled = true;
        }
        else
        {
            grid.enabled = false;
        }

        if (inThirdRoom == true)
        {
            paint.enabled = true;
        }
        else
        {
            paint.enabled = false;
        }

        if (inFourthRoom == true)
        {
            buttonRoom.enabled = true;
        }
        else
        {
            buttonRoom.enabled = false;
        }
	}
}
