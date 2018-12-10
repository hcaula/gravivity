using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGravityController : MonoBehaviour
{
    #region Public attributes
    public Vector3 gravityDirection;
    public float gravityForce;
    #endregion

    #region Private attributes
    private SwipeController sc;
    private Vector3 initialGravityDirection;
    #endregion

    void Start()
    {
        sc = GameObject.Find("Scene Manager").gameObject.GetComponent<SwipeController>();
        initialGravityDirection = gravityDirection;
    }

    void Update()
    {
        if (sc.SwipeUp || sc.SwipeDown ||
        sc.SwipeUpLeft || sc.SwipeUpRight ||
        sc.SwipeDownLeft || sc.SwipeDownRight) gravityDirection = CalculateDirection();

        /* Apply gravity force to each object that is affected by it */
        Physics.gravity = gravityDirection * gravityForce;
    }

    Vector3 CalculateDirection()
    {
		/* Since the Y-Axis is fixed, just check if there was a vertical swipe */
        if (sc.SwipeUp) return Vector3.up;
        if (sc.SwipeDown) return Vector3.down;

		/* Get camera perspective vectors */
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

		/* Put them on the same plane */
        forward.y = 0;
        right.y = 0;

		/* Check swipe direction */
		Vector3 ret = new Vector3();
		if (sc.SwipeUpRight) ret = Vector3.Normalize(forward + right);
		else if (sc.SwipeDownLeft) ret = Vector3.Normalize((forward + right) * -1);
		else if (sc.SwipeUpLeft) ret = Vector3.Normalize(forward - right);
		else if (sc.SwipeDownRight) ret = Vector3.Normalize((forward - right) * -1);

		/* In order to make a perpendicular vector */
		if (Mathf.Abs(ret.x) > Mathf.Abs(ret.z)) ret.z = 0f;
		else ret.x = 0f;

		return ret;
    }

    public void RestartGravity()
    {
        gravityDirection = initialGravityDirection;
    }
}
