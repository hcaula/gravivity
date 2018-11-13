using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	#region Public attributes
	public int rotationSpeed;
	#endregion

	#region Private attributes
	private bool isRotating;
	private DoubleTapController dtc;
	#endregion

	void Start()
	{
		/* Start this boolean as false */
		isRotating = false;

		/* Gets the Double Tab Controller component */
		dtc = GameObject.Find("Scene Manager").gameObject.GetComponent<DoubleTapController>();
	}

    void Update()
    {
		/* If the camera isn't in mid-rotation */
		if (!isRotating)
		{
			if (dtc.DtRight || Input.GetKeyDown("d")) Rotate("right");
			else if (dtc.DtLeft  || Input.GetKeyDown("a")) Rotate("left");
		}
    }

    void Rotate(string direction)
    {
		isRotating = true;

		/* Gets the camera's current Y-Axis value */
		int currentY = Mathf.RoundToInt(transform.rotation.eulerAngles.y);

		/* Adds or decreases 90 degrees to it */
		int targetY = direction == "left" ? currentY + 90 : currentY - 90;

		/* Calls rotation animation routine */
		StartCoroutine(AnimateRotation(currentY, targetY));
    }

	IEnumerator AnimateRotation(int currentY, int targetY)
	{
		/* Calculates the camera rotation step speed */
		int step = currentY > targetY ? -rotationSpeed : rotationSpeed;

		while (currentY != targetY)
		{
			yield return new WaitForSeconds(0.01f);
			currentY += step;
			transform.rotation = Quaternion.Euler(30, currentY, 0);
		}

		/* After animation is over, set rotating boolean as false */
		isRotating = false;
	}
}
