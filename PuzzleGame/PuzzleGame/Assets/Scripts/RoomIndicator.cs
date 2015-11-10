using UnityEngine;
using System.Collections;

public class RoomIndicator : MonoBehaviour {
    //Makes varibles needed
    public GameObject[] roomTriggers = new GameObject[4];
    public GameObject plane;
    ScriptEnabler scriptEnabler;

	// Use this for initialization
	void Start ()
    {
        //Gets script off of a plane
        scriptEnabler = plane.GetComponent<ScriptEnabler>();
	}

    void OnTriggerEnter(Collider trigger)
    {
        //Checks what trigger the player is in and enables certain scripts depending on what room the player is in
        if(roomTriggers[0].name == trigger.gameObject.name)
        {
            scriptEnabler.inFirstRoom = true;
        }

        if (roomTriggers[1].name == trigger.gameObject.name)
        {
            scriptEnabler.inSecondRoom = true;
            scriptEnabler.inFirstRoom = false;
        }

        if (roomTriggers[2].name == trigger.gameObject.name)
        {
            scriptEnabler.inThirdRoom = true;
            scriptEnabler.inFirstRoom = false;
            scriptEnabler.inSecondRoom = false;
        }

        if (roomTriggers[3].name == trigger.gameObject.name)
        {
            scriptEnabler.inFourthRoom = true;
            scriptEnabler.inThirdRoom = false;
            scriptEnabler.inFirstRoom = false;
            scriptEnabler.inSecondRoom = false;
        }
    }
}
