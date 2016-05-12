using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour {

	int random1, random2, amountToPay, amountGifted;
	public GameObject health, key, soul, objectToPay, objectGifted;
	bool playerNear, inventoryItem;

	// Use this for initialization
	void Start () 
	{
		random1 = Random.Range(1,4);
		random2 = Random.Range(1,5);
		inventoryItem = false;

		//Determine objects
		if (random1 == 1) 
		{
			objectToPay = health;

			if(random2 == 1)
			{
				random2 = Random.Range(2,5);
			}
			if (random2 == 2) 
			{
				objectGifted = key;
			}
			else if (random2 == 3) 
			{
				objectGifted = soul;
			}
			else if (random2 == 4) 
			{
				inventoryItem = true;
			}
		}
		else if (random1 == 2) 
		{
			objectToPay = key;

			if (random2 == 1 || random2 == 2) 
			{
				objectGifted = health;
			}
			else if (random2 == 3) 
			{
				objectGifted = soul;
			}
			else if (random2 == 4) 
			{
				inventoryItem = true;
			}
		}
		else if (random1 == 3) 
		{
			objectToPay = soul;

			if (random2 == 1 || random2 == 3) 
			{
				objectGifted = health;
			}
			else if (random2 == 2) 
			{
				objectGifted = key;
			}
			else if (random2 == 4) 
			{
				inventoryItem = true;
			}
		}

		//Determine costs
		if (objectToPay == health) 
		{
			if (objectGifted == key) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
			else if (objectGifted == soul) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
			else if (inventoryItem == true) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
		}
		else if (objectToPay == key) 
		{
			if (objectGifted == health) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
			else if (objectGifted == soul) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
			else if (inventoryItem == true) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
		}
		else if (objectToPay == soul) 
		{
			if (objectGifted == health) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
			else if (objectGifted == key) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
			else if (inventoryItem == true) 
			{
				amountToPay = 1;
				amountGifted = 1;
			}
		}
	}

	void Update()
	{
		if (objectToPay == key) 
		{
			if(Player.keys >= amountToPay)
			{
				if (playerNear && Input.GetKeyDown (KeyCode.E)) 
				{
					Use ();
				}
			}
		}

		if (objectToPay == health && Player.playerCurrentHealth >= amountToPay) 
		{
			if (playerNear && Input.GetKeyDown (KeyCode.E)) 
			{
				Use ();
			}
		}

		if (objectToPay == soul && Player.playerSouls >= amountToPay) 
		{
			if (playerNear && Input.GetKeyDown (KeyCode.E)) 
			{
				Use ();
			}
		}
	}

	void Use()
	{
		if (objectToPay == health) 
		{
			GM.playerReference.GetComponent<Player>().Damage(amountToPay);
		}
		else if (objectToPay == key) 
		{
			Player.RemoveKeys(amountToPay);
		}
		else if (objectToPay == soul) 
		{
			Player.RemoveSouls(amountToPay);
		}

		if (objectGifted == health) 
		{
			for(int i = 0; i < amountGifted; i++)
			{
				Instantiate(health, GM.PlayerCurrentLocation + new Vector3(4, 0, 0), Quaternion.identity);
			}
		}
		else if (objectGifted == key) 
		{
			for(int i = 0; i < amountGifted; i++)
			{
				GameObject keyThing = Instantiate(key, GM.PlayerCurrentLocation+ new Vector3(4, 0, 0), Quaternion.identity) as GameObject;
			}
		}
		else if (objectGifted == soul) 
		{
			for(int i = 0; i < amountGifted; i++)
			{
				Instantiate(soul, GM.PlayerCurrentLocation + new Vector3(4, 0, 0), Quaternion.identity);
			}
		}
		else if (inventoryItem == true) 
		{
			for(int i = 0; i < amountGifted; i++)
			{
				this.GetComponent<InventoryDropper>().ActivateDropper();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player") 
		{
			playerNear = true;
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.tag == "Player") 
		{
			playerNear = false;
		}
	}
}
