﻿using UnityEngine;
using System.Collections;

public class Puddle : Attack {

	void Start () 
	{
		StartCoroutine ("Timer");	
		damageAmount = 1;
		
		transform.Rotate(new Vector3(0,0,Random.Range (-180,180)));
	}
	
	IEnumerator Timer()
	{
		yield return new WaitForSeconds (5);
		Destroy(this.gameObject);
	}
}
