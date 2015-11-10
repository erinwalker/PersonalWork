using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisManager : MonoBehaviour {

    private float lastSpawned;
    List<GameObject> debrisList;
    List<GameObject> debrisToRemove;
    public GameObject debris;
    Vector3 screenToWorld;
    float randomNum;

    // Use this for initialization
    void Start()
    {
        this.screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        debrisList = new List<GameObject>();
        debrisToRemove = new List<GameObject>();
        lastSpawned = 0.0f;
        randomNum = Random.Range(-this.screenToWorld.y + .5f, this.screenToWorld.y - .5f);
        debrisList.Add(Instantiate(debris, new Vector3((screenToWorld.x + 2), randomNum, 0), Quaternion.identity) as GameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastSpawned + 2.0f)
        {
            lastSpawned = Time.time;
            randomNum = Random.Range(-this.screenToWorld.y + .5f, this.screenToWorld.y - .5f);
            debrisList.Add(Instantiate(debris, new Vector3((screenToWorld.x + 2), randomNum, 0), Quaternion.identity) as GameObject);
        }

        foreach (GameObject f in debrisList)
        {
            if (f.transform.position.x <= -screenToWorld.x - 2)
            {
                debrisToRemove.Add(f);
            }
        }

        foreach (GameObject ftr in debrisToRemove)
        {
            this.debrisList.Remove(ftr);
            Destroy(ftr);
        }
        debrisToRemove.Clear();
    }
}
