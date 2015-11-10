using UnityEngine;
using System.Collections;

public class Enemy : Shooter {
    //Set variables
    public GameObject player;
    Trigger triggerScript;
    Player playerScript;
    float timeDelay, delayAmount;
    bool destroyed;

	// Use this for initialization
	void Start ()
    {
        //Initiallizes variables
        triggerScript = player.GetComponent<Trigger>();
        playerScript = player.GetComponent<Player>();
        timeDelay = 0.0f;
        delayAmount = 1.0f;
        health = 1;
        destroyed = false;
	}

    //Delays the bullet coming out
    void DelayShoot()
    {
        if (Time.time > timeDelay)
        {
            timeDelay = Time.time + delayAmount;
            Shoot();
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Checks which zone player is in and makes the enemy look at the player and shoot
        position = spawnPoint.transform.position;
	    if(this.name == "Enemy1" && triggerScript.one == true)
        {
            this.transform.LookAt(player.transform);
            DelayShoot();      
        }
        if (this.name == "Enemy2" && triggerScript.two == true)
        {
            this.transform.LookAt(player.transform);
            DelayShoot();   
        }
        if (this.name == "Enemy3" && triggerScript.three == true)
        {
            this.transform.LookAt(player.transform);
            DelayShoot();    
        }
        if (this.name == "Enemy4" && triggerScript.four == true)
        {
            this.transform.LookAt(player.transform);
            DelayShoot(); 
        }
        //Deactivates enemy if shot
        if (health <= 0 && destroyed == false)
        {
            this.gameObject.SetActive(false);
            Debug.Log(this.name + "Destroyed");
            destroyed = true;
            playerScript.enemiesKilled++;
        }            
	}
}
