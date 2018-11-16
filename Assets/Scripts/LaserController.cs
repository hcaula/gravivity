﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	#region Public attributes
	public float rotationSpeed;
	#endregion

	#region Private attributes
    private ButtonInteractionController bic;
	private LineRenderer lr;
	private GameObject beam;
	private GameObject shooter;
	private Animator an;
	#endregion

    void Start()
    {
        bic = GetComponent<ButtonInteractionController>();
		an = GetComponent<Animator>();

		shooter = transform.GetChild(0).gameObject;
		beam = transform.GetChild(2).gameObject;
		lr = beam.GetComponent<LineRenderer>();
    }

    void Update()
    {
		
		/* If this laser is linked to a button, check if it's pressed before activating */
		/* If it's not linked, active always */
		if (bic && bic.GetIsActive()) 
		{
			lr.enabled = true;
			an.SetBool("isActive", true);
		}
		else {
			lr.enabled = false;
			an.SetBool("isActive", false);
		}

		/* Renders the laser */
		if (lr.enabled) CalculateBeam();

		/* Rotate shooter */
		shooter.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }

	void CalculateBeam()
	{
		lr.SetPosition(0, beam.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(beam.transform.position, beam.transform.forward, out hit))
		{
			/* If the raycast hits, laser ends where it hit */
			if (hit.collider) lr.SetPosition(1, hit.point);

		} else lr.SetPosition(1, beam.transform.forward * 5000);
	}
}
