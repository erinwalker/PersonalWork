using UnityEngine;
using System.Collections;

public class ButtonRoom : MonoBehaviour
{
    //Set Variables needed
    public GameObject[] button = new GameObject[4];
    public GameObject finalButton;
    public int buttonsPressed = 0;
    private bool finalActive = false;
    public bool finalPressed = false;
	
	// Update is called once per frame
	void Update () 
    {
        //Checks if all buttons have been pressd
        //and checks if the button is already active so it doesn't keep repeating
	    if(buttonsPressed == 4 && finalActive == false)
        {
            finalButton.SetActive(true);
            finalActive = true;
            Debug.Log("Final Button");
        }
	}
    
    
    void OnTriggerEnter(Collider buttonTouched)
    {
        //Checks if player is touching a button and moves it down and adds to the buttonsPressed count
        if(buttonTouched.gameObject.tag == "Button")
        {
            if (buttonTouched.gameObject == button[0] || button[1] || button[2] || button[3] && finalActive == false)
            {
                buttonTouched.gameObject.transform.position = new Vector3(buttonTouched.gameObject.transform.position.x, buttonTouched.gameObject.transform.position.y - .2f, buttonTouched.gameObject.transform.position.z);
                buttonsPressed++;
                Debug.Log("button pressed " + buttonsPressed);           
            }
        }

        //Checks if buttons are pressed to activate the final win button
        if(buttonTouched.gameObject.name == "FinalButton")
        {
            Debug.Log("You Win");
            finalPressed = true;
        }
    }
}
