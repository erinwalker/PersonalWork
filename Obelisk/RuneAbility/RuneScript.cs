using UnityEngine;
using System.Collections;

public class RuneScript : MonoBehaviour {

	public string RuneType;

	// Use this for initialization
	void Start () {
	
		switch(Random.Range (0,3))
		{
			case 0:
				//serenity
				GetComponent<Animator>().SetInteger("RuneType", 0);
				break;
			case 1:
				//rage
				GetComponent<Animator>().SetInteger("RuneType", 1);
				break;
			case 2:
				//sorrow
				GetComponent<Animator>().SetInteger("RuneType", 2);
				break;
		}
	
	}
	public void Pickup()
	{
		if(Player.playerRunes != 3)
		{
			Player.AddRune();
			GetComponent<Animator>().SetBool("PickedUp", true);
			GetComponent<Animator>().speed = 2;
			GetComponent<ParticleSystem>().Play ();
			StartCoroutine("BoomDestroy");
		}
	}
	
	IEnumerator BoomDestroy()
	{
		//turn off trigger
		GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(3);
		
		GetComponent<ParticleSystem>().Stop ();
		
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}
}
