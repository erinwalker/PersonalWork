using UnityEngine;
using System.Collections;

public class ObsidianMusicBox : InventoryItem {

	public GameObject musicBox;

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "Such a delicate tune to die to!";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO Drops a music box under the character that plays for X seconds, attracting all of the monsters in the room to its location for its duration.  
		//At the end of its duration it explodes dealing significant damage to all of the enemies in the AoE.
	}

	public override void Use ()
	{
		Instantiate (musicBox, GM.PlayerCurrentLocation, Quaternion.identity);
	}
}
