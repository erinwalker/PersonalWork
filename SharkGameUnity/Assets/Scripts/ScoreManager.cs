using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum ScoreState { Normal, Double, Paused};

public class ScoreManager : MonoBehaviour, ISubject {

    float score;
    public Text text;
    List<IObserver> observers;
    public ScoreState state;
    bool inDoubleMode;
    public AudioSource music, effects;
    public AudioClip underWater, superTime, eat, hit;
    static float highScore = 0;

    public ScoreState State
    {
        get { return state; }
        set
        {
            this.state = value;
            Notify();
        }
    }

	// Use this for initialization
	void Start ()
    {
        observers = new List<IObserver>();
        state = ScoreState.Normal;
        score = 0;
        inDoubleMode = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

        text.text = "Score: " + score.ToString() + "\nTime: " + (int)Time.timeSinceLevelLoad + "/60";

        if(score <= 0)
        {
            score = 0;
        }

        if ((int)Time.timeSinceLevelLoad >= 60)
        {
            state = ScoreState.Paused;
            if(score > highScore)
            {
                highScore = score;
            }
            text.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
            text.text = "Game Over\nScore: " + score.ToString() + "\nHigh Score: " + highScore + "\nPress R to restart.";
        }
        if(state == ScoreState.Paused)
        {
            Time.timeScale = 0;
        }
        if (state == ScoreState.Paused && Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("SharkGame");
            Time.timeScale = 1;
        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Fish" && c.GetComponent<Fish>().state.ToString() == "Super")//&& inDoubleMode == false
        {
            effects.clip = eat;
            effects.Play();
            inDoubleMode = true;
            State = ScoreState.Double;
            StartCoroutine("SuperTimer");
            float value = c.GetComponent<Fish>().fishValue;
            score += value;
            c.GetComponent<Renderer>().enabled = false;
        }
        else if(c.gameObject.tag == "Fish")
        {
            effects.clip = eat;
            effects.Play();
            float value = c.GetComponent<Fish>().fishValue;
            score += value;
            c.GetComponent<Renderer>().enabled = false;
        }
        else if(c.gameObject.tag == "Debris")
        {
            effects.clip = hit;
            effects.Play();
            int value = c.GetComponent<Debris>().debrisValue;
            score -= value;
            c.GetComponent<Renderer>().enabled = false;
            StartCoroutine("ColorTimer");
        }
    }

    IEnumerator ColorTimer()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.5f);
        if (State == ScoreState.Double)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }


    public void Attach(IObserver o)
    {
        observers.Add(o);
    }

    public void Detach(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify()
    {
        foreach (IObserver o in observers)
        {
            o.ObserverUpdate(this, state);
        }
    }

    IEnumerator SuperTimer()
    {
        this.GetComponent<SpriteRenderer>().color = Color.green;
        music.clip = superTime;
        music.Play();
        yield return new WaitForSeconds(6);
        music.clip = underWater;
        music.Play();
        State = ScoreState.Normal;
        inDoubleMode = false;
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
