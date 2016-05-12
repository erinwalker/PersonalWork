using UnityEngine;
using System.Collections;

public class Lantern : InventoryItem {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "Let there be light.";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Releases an aura of light around the character that stuns enemies in the radius for X seconds.

	}
}
