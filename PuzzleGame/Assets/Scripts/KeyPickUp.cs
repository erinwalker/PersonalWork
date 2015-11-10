using UnityEngine;
using System.Collections;

public class KeyPickUp : MonoBehaviour
{
    //Set Variables Needed
    public GameObject[] key;
    public bool hasKey = false;
    public GameObject currentKey;
    public GameObject player;

    //Function to pick up a ball
    void OnTriggerEnter(Collider keyUp)
    {
        if (hasKey == false)
        {
            switch (keyUp.gameObject.name)
            {
                //Checks which ball is selected and puts ball in middle of the screen and parents it to the player
                case "SphereBlue":
                    {
                        key[0].transform.parent = player.transform;
                        key[0].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + .25f, Camera.main.nearClipPlane + .50f));
                        hasKey = true;
                        currentKey = key[0];
                        break;
                    }
                case "SpherePurple":
                    {
                        key[1].transform.parent = player.transform;
                        key[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + .25f, Camera.main.nearClipPlane + .50f));
                        hasKey = true;
                        currentKey = key[1];
                        break;
                    }
                case "SphereRed":
                    {
                        key[2].transform.parent = player.transform;
                        key[2].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + .25f, Camera.main.nearClipPlane + .50f));
                        hasKey = true;
                        currentKey = key[2];
                        break;
                    }
                case "SphereYellow":
                    {
                        key[3].transform.parent = player.transform;
                        key[3].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + .25f, Camera.main.nearClipPlane + .50f));
                        hasKey = true;
                        currentKey = key[3];
                        break;
                    }
            }
        }
    }
}

