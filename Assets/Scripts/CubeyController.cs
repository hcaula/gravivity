using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeyController : MonoBehaviour {

	private Rigidbody rb;
	private GlobalGravityController gbc;
	private Vector3 gravity;
	private float gravityForce;

	void Start () 
	{
		/* Getting the global gravity from the Scene Manager */
		GameObject smObj = GameObject.Find("Scene Manager").gameObject;
		gbc = smObj.GetComponent<GlobalGravityController>();
		gravityForce = gbc.gravityForce;

		/* Initializing Rigidbody component */
		rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		gravity = gbc.gravityDirection;
		rb.AddForce(gravity * gravityForce);
	}
}
