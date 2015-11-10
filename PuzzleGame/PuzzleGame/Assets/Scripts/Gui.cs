using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour 
{
    //Sets variables needed
    ScriptEnabler script;
    public GameObject plane;
    ButtonRoom buttonScript;
    public GameObject player;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    void Start()
    {
        //Gets scripts off of player
        script = plane.GetComponent<ScriptEnabler>();
        buttonScript = player.GetComponent<ButtonRoom>();
    }

    void OnLevelWasLoaded(int level)
    {
        //Closes all doors
        door1.collider.enabled = true;
        door1.renderer.enabled = true;
        door2.collider.enabled = true;
        door2.renderer.enabled = true;
        door3.collider.enabled = true;
        door3.renderer.enabled = true;

        //Puts player in a new location when reloaded
        if (level == 0)
        {
            player.gameObject.transform.position = new Vector3(-4.25346f, 0.3297257f, -6.819225f);
            
        }
        if (level == 1)
        {
            player.gameObject.transform.position = new Vector3(-5.584248f, 0.3297257f, 1.863751f); 
        }
    }

	// Update is called once per frame
	void Update () 
    {
        //Stes Gui Text location
        this.guiText.pixelOffset = new Vector2(Screen.width*(-.455f), Screen.height*(.45f));
        
        //Changes gui text depending on what room the player is in
        if(script.inFirstRoom == true)
        {
            this.guiText.text = "Find all the colored spheres and return them to their bases.";
        }

        if (script.inSecondRoom == true)
        {
            this.guiText.text = "Navigate the correct path.";
        }

        if (script.inThirdRoom == true)
        {
            this.guiText.text = "Select a color and click to paint the gray pieces in the right pattern.";
        }

        //Checks if the player hit the final button
        //Changes gui text and reloads game
        if(buttonScript.finalPressed == true)
        {
            this.guiText.pixelOffset = new Vector2(-100, 50);
            this.guiText.fontSize = 50;
            this.guiText.text = "You Win!";
            Invoke("Reload", 2);
        }
        else if (script.inFourthRoom == true)
        {
            this.guiText.text = "Find and press the four buttons to unlock the final button to win.\nButtons Pressed: " + buttonScript.buttonsPressed;
        }
	}

    //Reload method
    void Reload()
    {
        Application.LoadLevel(0);
    }
}
