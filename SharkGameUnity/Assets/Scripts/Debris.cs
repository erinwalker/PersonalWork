using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {
    private float randomNum;
    public SpriteRenderer sr;
    public Sprite tire, soda, log;
    private Vector2 position, direction;
    private Vector3 moveTranslation;
    private float speed;
    private BoxCollider2D collider;
    public int debrisValue;

    // Use this for initialization
    void Start()
    {
        randomNum = (int)Random.Range(1, 4);
        direction = new Vector2(-0.3f, 0);
        collider = this.GetComponent<BoxCollider2D>();
        debrisValue = 1;

        if (randomNum == 1)
        {
            sr.sprite = tire;
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
            this.transform.localScale = new Vector3(.1f, .1f, .1f);
            collider.size = new Vector2(9, 9);
            speed = 8;

        }
        else if (randomNum == 2)
        {
            sr.sprite = soda;
            this.transform.localScale = new Vector3(.2f, .2f, .2f);
            collider.size = new Vector2(3, 5);
            speed = 10;

        }
        else if (randomNum == 3)
        {
            sr.sprite = log;
            this.transform.localScale = new Vector3(.5f, .5f, .5f);
            collider.size = new Vector2(5, 1);
            speed = 7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2(this.transform.position.x, this.transform.position.y);
        this.moveTranslation = new Vector3(this.direction.x, this.direction.y) * Time.deltaTime * speed;
        this.transform.position += new Vector3(this.moveTranslation.x, this.moveTranslation.y);
    }
}
