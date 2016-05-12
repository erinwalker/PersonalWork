using UnityEngine;
using System.Collections;

public class ScrollOfSummoningTandric : InventoryItem {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "I've returned for my shit!";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Summons Tandric’s vengeful spirit for x seconds.  
		//He is untargetable and enemies won’t react to his presence.  
		//He kills any enemy he touches during the duration.  
		//At the end of the duration any Tandric item the player has in their inventory will be lost.
	}
}
