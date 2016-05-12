using UnityEngine;
using System.Collections;

public class FrozenTalisman : InventoryItem {

	public GameObject collider;
	
	// Use this for initialization
	void Start () 
	{
		base.Start ();
	}

	public override void Use ()
	{
		StartCoroutine("Timer");
	}


	IEnumerator Timer()
	{
		Player.MoveLock ();
		Player.invulnerable = true;
		yield return new WaitForSeconds (5);
		Instantiate (collider, GM.PlayerCurrentLocation, Quaternion.identity);
		Player.invulnerable = false;
		Player.MoveUnlock ();
	}
}
