using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
    //Set variables to get health
    GameObject player, enemy1, enemy2, enemy3, enemy4;
    Player playerScript;
    Enemy[] enemyScript = new Enemy[4];

	// Use this for initialization
	void Start () 
    {
        //Initializes variables
            player = GameObject.Find("Player");
            enemy1 = GameObject.Find("Enemy1");
            enemy2 = GameObject.Find("Enemy2");
            enemy3 = GameObject.Find("Enemy3");
            enemy4 = GameObject.Find("Enemy4");
            playerScript = player.GetComponent<Player>();
            if (enemy1 != null)
                enemyScript[0] = enemy1.GetComponent<Enemy>();
            else
                enemyScript[0] = null;
            if (enemy2 != null)
                enemyScript[1] = enemy2.GetComponent<Enemy>();
            else
                enemyScript[1] = null;
            if (enemy3 != null)
                enemyScript[2] = enemy3.GetComponent<Enemy>();
            else
                enemyScript[2] = null;
            if (enemy4 != null)
                enemyScript[3] = enemy4.GetComponent<Enemy>();
            else
                enemyScript[3] = null;

	}

    void OnCollisionEnter(Collision hit)
    {
        //Subtracts health when someone is hit
            if (hit.collider.tag == "Player")
            {
                playerScript.health--;
            }

            if (hit.collider.tag == "Enemy")
            {
                Debug.Log(hit.collider.name + " hit");
                for(int i = 0; i <= 3; i++)
                {
                    if(hit.collider.name == "Enemy" + (i + 1))
                    {
                        enemyScript[i].health--;
                    }
                }
            }
    }
}
