using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public int rotationSpeed;
	private bool isRotating;
	private SwipeController sc;

	void Start()
	{
		isRotating = false;
		sc = GameObject.Find("Scene Manager").gameObject.GetComponent<SwipeController>();
	}

    void Update()
    {
		if (!isRotating)
		{
			if (sc.SwipeRight || Input.GetKeyDown("d")) Rotate("right");
			else if (sc.SwipeLeft  || Input.GetKeyDown("a")) Rotate("left");
		}
    }

    void Rotate(string direction)
    {
		isRotating = true;

		int currentY = Mathf.RoundToInt(transform.rotation.eulerAngles.y);
		int targetY = direction == "left" ? currentY + 90 : currentY - 90;

		StartCoroutine(AnimateRotation(currentY, targetY));
    }

	IEnumerator AnimateRotation(int currentY, int targetY)
	{
		int step = currentY > targetY ? -rotationSpeed : rotationSpeed;
		while (currentY != targetY)
		{
			yield return new WaitForSeconds(0.01f);
			currentY += step;
			transform.rotation = Quaternion.Euler(30, currentY, 0);
		}

		isRotating = false;
	}
}
