using UnityEngine;
using System.Collections;

public class IceforgedGreatsword : InventoryItem {

	int randomNumber = (int)Random.Range(1,100);
	
	// Update is called once per frame
	void Update () 
	{
		//TODO 5% chance on hit to be an instant kill on basic enemies
		if (randomNumber >= 1 && randomNumber <= 5) 
		{
		}
	}
}
