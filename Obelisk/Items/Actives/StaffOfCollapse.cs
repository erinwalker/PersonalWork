using UnityEngine;
using System.Collections;

public class StaffOfCollapse : InventoryItem {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "RNG, I believe in you.";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Disrupts the structural integrity of the cavern causing stalactites to fall randomly in the room dealing damage to anything hit, including the player.  
		//Shadows could indicate drop locations to allow for dodge mechanics.
	}
}
