using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapController : MonoBehaviour {
 
 	#region Public attributes
	public float margin;
	#endregion
	
	#region Private attributes
	private bool dtRight, dtLeft;
	#endregion

	#region Gets and Sets
	public bool DtLeft { get { return dtLeft; } }
    public bool DtRight { get { return dtRight; } }
	#endregion
	
	void Update () {
		/* Restart double tap variables */
		dtRight = dtLeft = false;
		
		/* If the player double tapped */
		if (Input.touches.Length > 0 && Input.touches[0].tapCount == 2)
		{
			float touchXPosition = Input.touches[0].position.x;
			int width = Screen.width;

			/* Check whether the double tap was at screen left ou right (plus margin) */
			if (touchXPosition < width/2 - margin) dtLeft = true;
			else if (touchXPosition > width/2 + margin) dtRight = true;
		}
	}
}
