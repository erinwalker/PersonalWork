using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FishState { Normal, Super };

public class Fish : MonoBehaviour, IObserver{

    private float randomNum;
    public SpriteRenderer sr;
    public Sprite blueFish, orangeFish, greenFish;
    private Vector2 position, direction;
    private Vector3 moveTranslation;
    private float speed;
    private BoxCollider2D collider;
    public FishState state;
    public float fishValue, originalValue;
    private ScoreManager sm;
    public GameObject Shark;


	// Use this for initialization
	void Start () 
    {
        Shark = GameObject.FindGameObjectWithTag("Shark");
        sm = Shark.GetComponent<ScoreManager>();
        randomNum = (int)Random.Range(1, 4);
        direction = new Vector2(-0.5f, 0);
        collider = this.GetComponent<BoxCollider2D>();

        if(randomNum == 1)
        {
            sr.sprite = blueFish;
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
            this.transform.localScale = new Vector3(.25f, .25f, .25f);
            collider.size = new Vector2(4, 3);
            speed = 10;
            originalValue = 1;
        }
        else if (randomNum == 2)
        {
            sr.sprite = orangeFish;
            this.transform.localScale = new Vector3(.25f, .25f, .25f);
            collider.size = new Vector2(4, 1.5f);
            speed = 8;
            originalValue = 2;
        }
        else if (randomNum == 3)
        {
            sr.sprite = greenFish;
            this.transform.localScale = new Vector3(.5f, .5f, .5f);
            collider.size = new Vector2(2, 2);
            speed = 6;
            originalValue = 3;
        }

        randomNum = (int)Random.Range(1, 10);

        if(randomNum == 1)
        {
            state = FishState.Super;
            sr.color = Color.green;
        }
        else
        {
            state = FishState.Normal;
        }
        sm.Attach(this);
        fishValue = originalValue;
	}
	
	// Update is called once per frame
	void Update () 
    {
        position = new Vector2(this.transform.position.x, this.transform.position.y);
        this.moveTranslation = new Vector3(this.direction.x, this.direction.y) * Time.deltaTime * speed;
        this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
	}


    public void ObserverUpdate(object sender, object message)
    {
        if(message.ToString() == "Double")
        {
            fishValue = originalValue * 2;
        }
        else
        {
            fishValue = originalValue;
        }
    }
}
