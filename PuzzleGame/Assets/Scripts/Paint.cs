using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {

    public GameObject[] colors = new GameObject[6];
    public GameObject[] picturePieces = new GameObject[6];
    GameObject currentColor;
    GameObject lastColor;
    Vector3 down;
    Vector3 up;
    RaycastHit hit;
    Ray ray;
    int afterOne;
    int coloredPieces;
    public GameObject door;
    bool[] colored = new bool[6];

	// Use this for initialization
	void Start () 
    {
        lastColor = new GameObject();
        afterOne = 0;
        coloredPieces = 0;

        for(int i = 0; i < colored.Length; i++)
        {
            colored[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0f));
        //Raycasts from the player to the gray pieces to color them
        if(Physics.Raycast(ray, out hit, 100))
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < picturePieces.Length; i++)
                {
                    if (currentColor == colors[i])
                    {
                        //Checks which gray piece is being hit
                        //Only reacts if the index is the same for the color and the piece
                        if (hit.collider.gameObject == picturePieces[i])
                        {
                            picturePieces[i].renderer.material = colors[i].renderer.material;
                            if (colored[i] == false)
                            {
                                coloredPieces++;
                                colored[i] = true;
                            }
                        }
                    }
                }
            }
        }

        //Opens door when all pieces are colored in the right way
        if(coloredPieces == 6)
        {
            door.collider.enabled = false;
            door.renderer.enabled = false;
        }
	}

    void OnTriggerEnter(Collider color)
    {
        if (color.gameObject.transform.tag == "Color")
        {
            GameObject temp = color.gameObject;
            //Sets vectors to go up and down
            down = new Vector3(color.gameObject.transform.position.x, color.gameObject.transform.position.y - .1f, color.gameObject.transform.position.z);
            up = new Vector3(lastColor.gameObject.transform.position.x, lastColor.gameObject.transform.position.y + .1f, lastColor.gameObject.transform.position.z);

            //Switches color the player can paint with
            switch (color.gameObject.name)
            {
                case "Red":
                    {
                        //makes sure the current and last color are not the same so the buttons dont float in the air
                        //Then moves the button up and down if they are not the same
                        if (color.gameObject != lastColor)
                        {
                            color.gameObject.transform.position = down;
                            lastColor.transform.position = up;
                        }
                        currentColor = color.gameObject;
                        break;
                    }
                case "Orange":
                    {
                        if (color.gameObject != lastColor)
                        {
                            color.gameObject.transform.position = down;
                            lastColor.transform.position = up;
                        }
                        currentColor = color.gameObject;
                        break;
                    }
                case "Yellow":
                    {
                        if (color.gameObject != lastColor)
                        {
                            color.gameObject.transform.position = down;
                            lastColor.transform.position = up;
                        }
                        currentColor = color.gameObject;
                        break;
                    }
                case "Green":
                    {
                        if (color.gameObject != lastColor)
                        {
                            color.gameObject.transform.position = down;
                            lastColor.transform.position = up;
                        }
                        currentColor = color.gameObject;
                        break;
                    }
                case "Blue":
                    {
                        if (color.gameObject != lastColor)
                        {
                            color.gameObject.transform.position = down;
                            lastColor.transform.position = up;
                        }
                        currentColor = color.gameObject;
                        break;
                    }
                case "Purple":
                    {
                        if (color.gameObject != lastColor)
                        {
                            color.gameObject.transform.position = down;
                            lastColor.transform.position = up;
                        }
                        currentColor = color.gameObject;
                        break;
                    }
            }

            afterOne++;

            //Sets the previous color so it can be used later to move up and down
            if (afterOne >= 1)
            {
                lastColor = temp;
            }           
        }
    }
}
