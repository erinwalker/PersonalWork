using UnityEngine;
using System.Collections;

public class BootsOfZooming2 : InventoryItem {

	public override void Use ()
	{
		StartCoroutine ("SpeedTimer", 10);
	}
	
	IEnumerator SpeedTimer(int time)
	{
		Player.bonusMobility += 5;
		Player.UpdateStats ();
		
		yield return new WaitForSeconds(time);
		
		Player.bonusMobility -= 5;
		Player.UpdateStats ();
	}
}
