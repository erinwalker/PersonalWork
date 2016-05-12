using UnityEngine;
using System.Collections;

public class DeekooSmokeNut : InventoryItem {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "No correlation";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Shrouds the user making them invisible for X seconds.  
		//Attacks and actions remove the effect instantly, but enemies lose aggro and either stay stationary or wander randomly for the duration.
	}

	public override void Use ()
	{
		StartCoroutine ("Timer");
	}

	IEnumerator Timer()
	{
		//Turn Invisible

		yield return new WaitForSeconds (10);

		//Stop being invisible
	}

}
