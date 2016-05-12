using UnityEngine;
using System.Collections;

public class CandleTrap : InventoryItem {

	public GameObject candle;
	GameObject temp;

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		ItemDescription = "You take candle!";
	}

	public override void Use ()
	{
		temp = Instantiate (candle, GM.PlayerCurrentLocation, Quaternion.identity)as GameObject;
		StartCoroutine ("Timer");

	}

	IEnumerator Timer()
	{
		foreach (Enemy enemy in GameState.enemies)
		{
			//enemy.previousTarget = enemy.currentTarget;
			enemy.currentTarget = temp;
		}

		yield return new WaitForSeconds (5);
		Destroy (temp);
		foreach (Enemy enemy in GameState.enemies)
		{
			enemy.currentTarget = GM.playerReference.gameObject;
			//enemy.previousTarget = temp;
		}
	}
}
