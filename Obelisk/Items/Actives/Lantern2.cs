using UnityEngine;
using System.Collections;

public class Lantern2 : InventoryItem {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "Let there be more light.";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Releases an aura of light (with significantly larger radius than Lantern I) around the character that stuns enemies in the radius for X seconds.

	}
}
