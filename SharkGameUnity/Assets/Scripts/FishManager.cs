using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishManager : MonoBehaviour {

    private float lastSpawned;
    List<GameObject> fishList;
    List<GameObject> fishToRemove;
    public GameObject fish;
    Vector3 screenToWorld;
    float randomNum;

	// Use this for initialization
	void Start () 
    {
        this.screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        fishList = new List<GameObject>();
        fishToRemove = new List<GameObject>();
        lastSpawned = 0.0f;
        randomNum = Random.Range(-this.screenToWorld.y + .5f, this.screenToWorld.y - .5f);
        fishList.Add(Instantiate(fish, new Vector3((screenToWorld.x + 2), randomNum, 0), Quaternion.identity) as GameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Time.time >= lastSpawned + 1.0f)
        {
            lastSpawned = Time.time;
            randomNum = Random.Range(-this.screenToWorld.y + .5f, this.screenToWorld.y - .5f);
            fishList.Add(Instantiate(fish, new Vector3((screenToWorld.x + 2), randomNum, 0), Quaternion.identity) as GameObject);
        }

        foreach (GameObject f in fishList)
        {
            if (f.transform.position.x <= -screenToWorld.x - 2)
            {
                fishToRemove.Add(f);
            }
        }

        foreach (GameObject ftr in fishToRemove)
        {
            this.fishList.Remove(ftr);
            Destroy(ftr);
        }
        fishToRemove.Clear();
	}
}
