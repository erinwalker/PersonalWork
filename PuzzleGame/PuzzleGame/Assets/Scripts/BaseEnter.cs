using UnityEngine;
using System.Collections;

public class BaseEnter : MonoBehaviour {
    //Set variables
    public GameObject player;
    private KeyPickUp pickUpScript;
    public int numberOfBasesActive = 0;
    public GameObject[] bases;
    private Shader shader;
    public GameObject door;

	// Use this for initialization
	void Start ()
    {
        pickUpScript = player.GetComponent<KeyPickUp>();
        shader = Shader.Find("Diffuse");
	}

    //Checks to see if the win condition is met and opens door if won
    void Update()
    {
        if(numberOfBasesActive == 4)
        {
            door.collider.enabled = false;
            door.renderer.enabled = false;
        }
    }

    //Checks to see if base and ball match same color based on index in array
    //Destroys ball if the right base is entered andallows player to find the next one
    void OnTriggerEnter(Collider pole)
    {
        if(pole.gameObject == bases[0] && pickUpScript.currentKey == pickUpScript.key[0])
        {
            numberOfBasesActive++;
            pickUpScript.hasKey = false;
            pole.renderer.material.shader = shader;
            pole.isTrigger = false;
            Destroy(pickUpScript.key[0]);
        }
        if (pole.gameObject == bases[1] && pickUpScript.currentKey == pickUpScript.key[1])
        {
            numberOfBasesActive++;
            pickUpScript.hasKey = false;
            pole.renderer.material.shader = shader;
            pole.isTrigger = false;
            Destroy(pickUpScript.key[1]);
        }
        if (pole.gameObject == bases[2] && pickUpScript.currentKey == pickUpScript.key[2])
        {
            numberOfBasesActive++;
            pickUpScript.hasKey = false;
            pole.renderer.material.shader = shader;
            pole.isTrigger = false;
            Destroy(pickUpScript.key[2]);
        }
        if (pole.gameObject == bases[3] && pickUpScript.currentKey == pickUpScript.key[3])
        {
            numberOfBasesActive++;
            pickUpScript.hasKey = false;
            pole.renderer.material.shader = shader;
            pole.isTrigger = false;
            Destroy(pickUpScript.key[3]);
        }
    }
}
