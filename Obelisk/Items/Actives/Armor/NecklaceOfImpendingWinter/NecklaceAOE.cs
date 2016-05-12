using UnityEngine;
using System.Collections;

public class NecklaceAOE : Attack {

	void Start () 
	{
		StartCoroutine ("Timer");
		damageAmount = 1;
	}
	
	IEnumerator Timer()
	{
		yield return new WaitForSeconds (5);
		Destroy(this.gameObject);
	}
}
