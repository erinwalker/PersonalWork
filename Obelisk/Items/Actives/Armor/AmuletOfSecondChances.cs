using UnityEngine;
using System.Collections;

public class AmuletOfSecondChances : InventoryItem {

	public override void Use ()
	{
		if (Player.playerCurrentHealth <= 5) 
		{
			StartCoroutine ("InvulTimer");
		}
	}

	IEnumerator InvulTimer()
	{
		Player.invulnerable = true;
		yield return new WaitForSeconds (3);
		Player.invulnerable = false;
	}
}
