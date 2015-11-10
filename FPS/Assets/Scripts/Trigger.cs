using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour 
{
    //Set variables
    public GameObject triggerOne, triggerTwo, triggerThree, triggerFour;
    public bool one, two, three, four;

	// Use this for initialization
	void Start () 
    {
        //initially set each trigger zone to false
        one = false;
        two = false;
        three = false;
        four = false;
	}

    //Checks when a trigger zone is entered and changes the bool accordingly
    void OnTriggerEnter(Collider other)
    {
        if (other.name == triggerOne.name)
            one = true;
        if (other.name == triggerTwo.name)
            two = true;
        if (other.name == triggerThree.name)
            three = true;
        if (other.name == triggerFour.name)
            four = true;
    }

    //Checks when a trigger zone is exited and changes the bool accordingly
    void OnTriggerExit(Collider other)
    {
        if (other.name == triggerOne.name)
            one = false;
        if (other.name == triggerTwo.name)
            two = false;
        if (other.name == triggerThree.name)
            three = false;
        if (other.name == triggerFour.name)
            four = false;
    }
}
