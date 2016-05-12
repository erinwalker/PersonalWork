using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryDropper : MonoBehaviour {

	//GameObject Lists
	public List<GameObject> MineralList;
	public List<GameObject> SpookyList;
	public List<GameObject> GrowthList;
	public List<GameObject> FreljordList;
	public List<GameObject> ObsidianList;

	public GameObject itemSelected;

	//items
	[HideInInspector] public GameObject myLuckyCoin;
	[HideInInspector] public GameObject myHeart; 
	[HideInInspector] public GameObject myFireTome;
	[HideInInspector] public GameObject myFireTome2; 
	[HideInInspector] public GameObject myFireTome3;
	[HideInInspector] public GameObject myIceTome; 
	[HideInInspector] public GameObject myIceTome2;
	[HideInInspector] public GameObject myLightningTome; 
	[HideInInspector] public GameObject myLightningTome2; 
	[HideInInspector] public GameObject myPotion; 
	[HideInInspector] public GameObject myExplosiveCharge; 
	//[HideInInspector] public GameObject myCandleTrap; 
	[HideInInspector] public GameObject myDrill;
	[HideInInspector] public GameObject myFrozenTalisman;

	//Weapons
	[HideInInspector] public GameObject myAxeOfBatButchering;
	[HideInInspector] public GameObject myAxeOfTheFirstWinter;
	[HideInInspector] public GameObject myClaymore;
	[HideInInspector] public GameObject myDragonGlassDagger; 
	[HideInInspector] public GameObject myFrostQueenComb;
	[HideInInspector] public GameObject myIceforgedGreatsword;
	[HideInInspector] public GameObject myKoeglersSwordOfTheCrusade;
	[HideInInspector] public GameObject myMineCartHammer;
	[HideInInspector] public GameObject myMinePick;
	[HideInInspector] public GameObject myPauldronsOfNeverSlime; 
	[HideInInspector] public GameObject myRustyShovel; 
	[HideInInspector] public GameObject mySteelSword; 
	[HideInInspector] public GameObject myTandricsMachete;
	[HideInInspector] public GameObject myTandricsTorch;
	[HideInInspector] public GameObject myTandricsTwoHandedHammer;
	[HideInInspector] public GameObject myTridentOfSquidSlaying;
	[HideInInspector] public GameObject myVineWhip;
	[HideInInspector] public GameObject myWoodenSword;

	//Armor
	[HideInInspector] public GameObject myAmuletOfSecondChances;
	[HideInInspector] public GameObject myArmorOfThorns;
	[HideInInspector] public GameObject myBraceletOfControl;
	[HideInInspector] public GameObject myCarvernArmor;
	[HideInInspector] public GameObject myNecklaceOfImpendingWinter;
	[HideInInspector] public GameObject myTandricsFurryArmor;
	[HideInInspector] public GameObject myBootsOfZooming;
	[HideInInspector] public GameObject myBootsOfZooming2;

	void Start()
	{
		//Mineral list items
		MineralList.Add (myBraceletOfControl);
		MineralList.Add (myBootsOfZooming);
		MineralList.Add (myLuckyCoin);
		MineralList.Add (myHeart);
		MineralList.Add (myFireTome);
		MineralList.Add (myIceTome);
		MineralList.Add (myLightningTome);
		MineralList.Add (myPotion);
		MineralList.Add (myRustyShovel);
		MineralList.Add (myVineWhip);
		MineralList.Add (myWoodenSword);


		//Spooky list items
		SpookyList.Add (myCarvernArmor);
		SpookyList.Add (myFireTome2);
		SpookyList.Add (myIceTome2);
		SpookyList.Add (myLightningTome2);
		SpookyList.Add (myAxeOfBatButchering);
		SpookyList.Add (myMineCartHammer);
		SpookyList.Add (myMinePick);
		SpookyList.Add (myTandricsTorch);
		SpookyList.Add (myTridentOfSquidSlaying);
		//SpookyList.Add (myCandleTrap);

		//Growth list items
		GrowthList.Add (myArmorOfThorns);
		GrowthList.Add (myBootsOfZooming2);
		GrowthList.Add (myExplosiveCharge);
		GrowthList.Add (myClaymore);
		GrowthList.Add (myKoeglersSwordOfTheCrusade);
		GrowthList.Add (myPauldronsOfNeverSlime);
		GrowthList.Add (mySteelSword);
		GrowthList.Add (myTandricsMachete);
		GrowthList.Add (myDrill);

		//Freljord list items
		FreljordList.Add (myNecklaceOfImpendingWinter);
		FreljordList.Add (myTandricsFurryArmor);
		FreljordList.Add (myAxeOfTheFirstWinter);
		FreljordList.Add (myFrostQueenComb);
		FreljordList.Add (myIceforgedGreatsword);
		FreljordList.Add (myFrozenTalisman);

		//Obsidian list items
		ObsidianList.Add (myAmuletOfSecondChances);
		ObsidianList.Add (myFireTome3);
		ObsidianList.Add (myDragonGlassDagger);
		ObsidianList.Add (myTandricsTwoHandedHammer);
	}

	public void ActivateDropper()
	{
		DropWhichObject ();
	}
	
	void DropWhichObject()
	{
	
		//else if (this.gameObject.tag == "Boss")
		//	boss loot
		
		Vector3 dropPoint = new Vector3(transform.position.x,
		                                transform.position.y,
		                                0f);
		/*
		if(choiceChance == 1)
		{
			//nothin
		}
		*/
		//an inventory item
		if (GameState.CurrentBiome == GameState.BiomeLevel.Mineral)
		{
			int R;

			R = Random.Range(0, MineralList.Count);
			itemSelected = Instantiate(MineralList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
		}

		if (GameState.CurrentBiome == GameState.BiomeLevel.Echo)
		{
			int R;

			R = Random.Range(0, 2);

			
			if (R == 0)
			{
				R = Random.Range(0, MineralList.Count);
				itemSelected = Instantiate(MineralList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

			if (R == 1)
			{
				R = Random.Range(0, SpookyList.Count);
				itemSelected = Instantiate(SpookyList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

		}



		if (GameState.CurrentBiome == GameState.BiomeLevel.Growth)
		{
			int R;
			
			R = Random.Range(0, 4);
				
				
			if (R == 0)
			{
				R = Random.Range(0, MineralList.Count);
				itemSelected = Instantiate(MineralList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			
			if (R == 1)
			{
				R = Random.Range(0, SpookyList.Count);
				itemSelected = Instantiate(SpookyList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

			if (R == 2 || R == 3)
			{
				R = Random.Range(0, GrowthList.Count);
				itemSelected = Instantiate(GrowthList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			

		}

		if (GameState.CurrentBiome == GameState.BiomeLevel.Freljlord)
		{
			int R;
			
			R = Random.Range(0, 6);
				
				
			if (R == 0)
			{
				R = Random.Range(0, MineralList.Count);
				itemSelected = Instantiate(MineralList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			
			if (R == 1)
			{
				R = Random.Range(0, SpookyList.Count);
				itemSelected = Instantiate(SpookyList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			
			if (R == 2 || R == 3)
			{
				R = Random.Range(0, GrowthList.Count);
				itemSelected = Instantiate(GrowthList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

			if (R == 4 || R == 5)
			{
				R = Random.Range(0, FreljordList.Count);
				itemSelected = Instantiate(FreljordList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

		}

		if (GameState.CurrentBiome == GameState.BiomeLevel.Obsidian)
		{
			int R;
			
			R = Random.Range(0, 8);
				
				
			if (R == 0)
			{
				R = Random.Range(0, MineralList.Count);
				itemSelected = Instantiate(MineralList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			
			if (R == 1)
			{
				R = Random.Range(0, SpookyList.Count);
				itemSelected = Instantiate(SpookyList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			
			if (R == 2 || R == 3)
			{
				R = Random.Range(0, GrowthList.Count);
				itemSelected = Instantiate(GrowthList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}
			
			if (R == 4 || R == 5)
			{
				R = Random.Range(0, FreljordList.Count);
				itemSelected = Instantiate(FreljordList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

			if (R == 6 || R == 7)
			{
				R = Random.Range(0, FreljordList.Count);
				itemSelected = Instantiate(ObsidianList[R], dropPoint, Quaternion.Euler(0,0,0))as GameObject;
			}

			
		}

		PushALittle (itemSelected);

	}

	void PushALittle(GameObject item)
	{
		//push the thing a little left or right
		if(Random.Range (0,2) == 1)
		{
			//everything you own in a box to the left
			item.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-0.5f, -2.0f), 0), ForceMode2D.Impulse);
			//random for y
			
		}
		else
		{
			//or.. to the right
			item.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (0.5f, 2.0f), 0), ForceMode2D.Impulse);
		}
		
		//push the thing a little up or down
		if(Random.Range (0,2) == 1)
		{
			//everything you own in a box.. above?
			item.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Random.Range (0.5f,2.0f)), ForceMode2D.Impulse);
		}
		else
		{
			//or.. down.. god.. fine.
			item.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Random.Range (-0.5f,-2.0f)), ForceMode2D.Impulse);
		}
	}

	IEnumerator ColliderHandling(GameObject g)
	{
		yield return new WaitForSeconds (1f);
		g.GetComponent<BoxCollider2D>().enabled = true;
	}
}
