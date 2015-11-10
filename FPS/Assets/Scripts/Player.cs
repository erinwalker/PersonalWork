using UnityEngine;
using System.Collections;

public class Player : Shooter {
    //Set variables
    public GUIText text;
    public int enemiesKilled;
    public GameObject door, key;
    public Light one, two, three, four;
    bool hasKey;

	// Use this for initialization
	void Start ()
    {
        health = 10;
        enemiesKilled = 0;
        door.SetActive(false);
        one.enabled = false;
        two.enabled = false;
        three.enabled = false;
        four.enabled = false;
        key.SetActive(false);
        hasKey = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        text.text = "Health: " + health;
        position = spawnPoint.transform.position;

        //Lets the player shoot and tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
            Shoot();

        //Reloads level if health is zero
        if (health <= 0)
        {
            text.anchor = TextAnchor.MiddleCenter;
            text.transform.position = new Vector3(0.5f, 0.5f, 0f);
            text.fontSize = 50;
            text.text = "You Lose!";
            Invoke("Reload", 2f);
        }
        
        //Checks how many enemies were killed and activates the final door
        if(enemiesKilled == 4)
        {
            door.SetActive(true);
            one.enabled = true;
            two.enabled = true;
            three.enabled = true;
            four.enabled = true;
            key.SetActive(true);
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.transform.position = new Vector3(0.5f, 0.5f, 0f);
            text.fontSize = 50;
            text.text = "You Win!\nPick up key and exit to restart.";
        }
	}

    //Checks which trigger the player is in
    void OnTriggerEnter(Collider other)
    {
        //If player is in the key the player picks it up
        if(other == key.GetComponent<Collider>())
        {
            key.transform.parent = this.transform;
            key.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + .25f, Camera.main.nearClipPlane + .50f));
            hasKey = true;
        }

        //The player must have the key to restart level when going through door
        if(other == door.GetComponent<Collider>() && hasKey == true)
        {
            Application.LoadLevel("FPS");
        }
    }

    void Reload()
    {
        Application.LoadLevel("FPS");
    }
}
