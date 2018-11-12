using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGravityController : MonoBehaviour
{

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
        // forward = Camera.main.transform.forward;

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

        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        Vector3 upRight = Vector3.Normalize(forward + right);
        Vector3 downLeft = Vector3.Normalize(upRight * -1);
        Vector3 upLeft = Vector3.Normalize(forward - right);
        Vector3 downRight = Vector3.Normalize(upLeft * -1);

        if (sc.SwipeUp) ChangeGravity("up", null);
        else if (sc.SwipeDown) ChangeGravity("down", null);
		else if (sc.SwipeUpLeft) print(upLeft);
		else if (sc.SwipeUpRight) print(upRight);
		else if (sc.SwipeDownLeft) print(downLeft);
		else if (sc.SwipeDownRight) print(downRight);
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
