using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundController : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			DeathController dc = col.gameObject.GetComponent<DeathController>();
            dc.Die();
		}
	}
}
