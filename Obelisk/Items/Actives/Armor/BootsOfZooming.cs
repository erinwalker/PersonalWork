using UnityEngine;
using System.Collections;

public class BootsOfZooming : InventoryItem {

	public override void Use ()
	{
		StartCoroutine ("SpeedTimer", 5);
	}

	IEnumerator SpeedTimer(int time)
	{
		Player.bonusMobility += 2;
		Player.UpdateStats ();

		yield return new WaitForSeconds(time);

		Player.bonusMobility -= 2;
		Player.UpdateStats ();
	}
}
