using UnityEngine;
using System.Collections;

public class EntanglingRoots : InventoryItem {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "Eat more vegetables.";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO All enemies on screen get snared for X seconds and take damage on activation and over the root duration.
		StartCoroutine ("Timer");
	}

	IEnumerator Timer()
	{
		//Get snared

		yield return new WaitForSeconds (10);
		
		//Stop snare
	}
}
