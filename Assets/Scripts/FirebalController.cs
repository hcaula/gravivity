﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebalController : MonoBehaviour {

    public float maxSpeed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        /* Limit Cubey's speed */
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            DeathController dc = col.gameObject.GetComponent<DeathController>();
            dc.Die();
        }
    }
}
