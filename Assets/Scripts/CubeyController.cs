using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeyController : MonoBehaviour
{
    public float maxSpeed;

	private Rigidbody rb;
    private DeathController dc;

	void Start()
    {
        rb = GetComponent<Rigidbody>();
        dc = GetComponent<DeathController>();
    }

    void Update()
    {
		/* Limit Cubey's speed */
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
