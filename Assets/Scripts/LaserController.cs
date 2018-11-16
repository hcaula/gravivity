using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	#region Public attributes
	#endregion

	#region Private attributes
    private ButtonInteractionController bic;
	private LineRenderer lr;
	#endregion

    void Start()
    {
        bic = GetComponent<ButtonInteractionController>();
		lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
		/* If this laser is linked to a button, check if it's pressed before activating */
		/* If it's not linked, active always */
        if ((bic && bic.GetIsActive()) || !bic) ActivateBeam();
    }

    void ActivateBeam()
    {
		lr.SetPosition(0, this.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position, transform.forward, out hit))
		{
			if (hit.collider)
			{
				lr.SetPosition(1, hit.point);
			}
		} else lr.SetPosition(1, this.transform.forward * 5000);
    }
}
