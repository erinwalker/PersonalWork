using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    public float Speed = 0.1f;
    private bool stoppedOnce = false;
	public float bottLoc = 0;
	public float xPos = 0;

	void Start()
	{
		this.transform.localPosition = new Vector3 (xPos, bottLoc, 0);
	}

	// Update is called once per frame
	void Update () 
    {
        if(UIFunctions.OnCredits)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+(Speed*Time.deltaTime), this.transform.position.z);

        if(this.transform.localPosition.y >= 3000 && stoppedOnce == false)
        {
            UIFunctions.OnCredits = false;
            stoppedOnce = true;
        }
	}

    public void BackToHomeAfterCredits()
    {
        this.transform.localPosition = new Vector3(xPos, bottLoc, 0);
        stoppedOnce = false;
    }
}
