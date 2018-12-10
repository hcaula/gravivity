using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundController : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		print("hello!1");
		if (col.gameObject.tag == "Player")
		{
			print("hello!2");
			DeathController dc = col.gameObject.GetComponent<DeathController>();
            dc.Die();
		}
	}
}
