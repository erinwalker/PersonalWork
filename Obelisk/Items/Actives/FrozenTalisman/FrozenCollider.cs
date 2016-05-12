using UnityEngine;
using System.Collections;

public class FrozenCollider : Attack {

	bool grow = true;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("ColliderTimer");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (grow) 
		{
			this.GetComponent<CircleCollider2D>().radius += .5f * Time.deltaTime;
		}
	}
	
	IEnumerator ColliderTimer()
	{	
		yield return new WaitForSeconds (3);
		grow = false;
		Destroy(this.gameObject);
	}
}
