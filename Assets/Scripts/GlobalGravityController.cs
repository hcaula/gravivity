using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGravityController : MonoBehaviour {
	
	public Vector3 gravityDirection;
	public float gravityForce;

	private Vector3 forward, right, up;
	private SwipeController sc;

	void Start()
	{
		sc = GameObject.Find("Scene Manager").gameObject.GetComponent<SwipeController>();
	}

	void Update()
	{
		// /* Getting the camera's forward direction vector */
		forward = Camera.main.transform.forward;

		// /* Forcing the y vector direction as 0 */
		// forward.y = 0;

		// /* Normalizing forward direction for padronization */
		// forward = Vector3.Normalize(forward);

		// /* Setting the right vector based on the forward direction */
		// right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

		// Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
		// Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

		// Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		// transform.forward = heading;
		// transform.position += rightMovement;
		// transform.position += upMovement;

		if (sc.SwipeUp) 
		{
			print(forward);
			ChangeGravity("up", null);
		}
		else if (sc.SwipeDown) ChangeGravity("down", null);
	}

	void ChangeGravity(string vertical, string horizontal)
	{
		if (vertical == "up")
		{
			if (string.IsNullOrEmpty(horizontal)) gravityDirection = Vector3.up;
		} 
		else
		{
			if (string.IsNullOrEmpty(horizontal)) gravityDirection = Vector3.down;
		}
	}
}
