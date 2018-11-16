using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	#region Public attributes
	#endregion

	#region Private attributes
    private ButtonInteractionController bic;
	private LineRenderer lr;
	private GameObject beam;
	#endregion

    void Start()
    {
        bic = GetComponent<ButtonInteractionController>();

		beam = transform.GetChild(2).gameObject;
		lr = beam.GetComponent<LineRenderer>();
    }

    void Update()
    {
		/* If this laser is linked to a button, check if it's pressed before activating */
		/* If it's not linked, active always */
		if (bic && bic.GetIsActive()) lr.enabled = true;
		else lr.enabled = false;

		/* Renders the laser */
		if (lr.enabled) CalculateBeam();
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
