using UnityEngine;
using System.Collections;

public class Potion : InventoryItem {

	public override void Use ()
	{
		base.Use ();
		Player.playerReference.GetComponent<Player>().Heal(2);
	}
}
