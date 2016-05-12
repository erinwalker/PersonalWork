using UnityEngine;
using System.Collections;

public class NecklaceOfImpendingWinter : InventoryItem {

	public GameObject necklaceAOE;
	GameObject temp;
	
	public override void Use ()
	{
		temp = Instantiate (necklaceAOE, GM.PlayerCurrentLocation, Quaternion.identity) as GameObject;
		
	}	
	// Update is called once per frame
	void Update () 
	{
		//TODO Creates a large AoE field around the player that slows and damages enemies over its duration (x seconds).
		if (temp != null) 
		{
			temp.transform.position = GM.PlayerCurrentLocation;
		}
	}
}
