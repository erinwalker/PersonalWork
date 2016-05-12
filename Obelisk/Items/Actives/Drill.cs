using UnityEngine;
using System.Collections;

public class Drill : InventoryItem {

	bool active;

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "Be like a mole.";
	}

	public override void Use ()
	{
		if (active == true) {
			Debug.Log ("Not Invincible");
			InventoryManager.ableToSwitch = true;
			Player.invulnerable = false;
			active = false;
		} 
		else 
		{
			StartCoroutine ("Timer");
		}
	}

	IEnumerator Timer()
	{
		InventoryManager.ableToSwitch = false;
		Player.invulnerable = true;
		Debug.Log ("Invincible");
		active = true;
		yield return new WaitForSeconds (5);
		active = false;
		InventoryManager.ableToSwitch = true;
		Player.invulnerable = false;

	}
}
