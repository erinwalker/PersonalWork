using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {

    private InputController controller;
    private Vector2 direction = new Vector2(0, 0);
    private float speed = 10;
    private Vector3 moveTranslation;
    private Vector3 screenToWorld;
    private Vector2 position;

	// Use this for initialization
	void Start () 
    {
        controller = GetComponent<InputController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        position = new Vector2(this.transform.position.x, this.transform.position.y);
        this.direction = this.controller.direction;
        this.moveTranslation = new Vector3(this.direction.x, this.direction.y) * Time.deltaTime * speed;
        this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
        this.screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (this.transform.position.y >= this.screenToWorld.y - .75f)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.screenToWorld.y - .75f);
        }
        else if (this.transform.position.y <= -this.screenToWorld.y + .75f)
        {
            this.transform.position = new Vector2(this.transform.position.x, -this.screenToWorld.y + .75f);
        }
	}
}
