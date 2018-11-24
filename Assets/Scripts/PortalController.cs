using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    public PortalController exitPortal;
    public string exitDirection;
    public bool lockRotation;
    private Vector3 direction;
    private float minimumDistance = 2.0f;
    private float exitSpeed = 1.0f;
    private Rigidbody rb;
    private GlobalGravityController globalGravController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        switch (exitDirection)
        {
            case "east":
                direction = new Vector3(1,0,0);
                break;

            case "west":
                direction = new Vector3(-1, 0, 0);
                break;

            case "north":
                direction = new Vector3(0, 0, 1);
                break;

            case "south":
                direction = new Vector3(0, 0, -1);
                break;

            case "up":
                direction = new Vector3(0, 1, 0);
                break;

            case "down":
                direction = new Vector3(0, -1, 0);
                break;

            default:

                break;


        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = exitPortal.transform.position + direction * minimumDistance;
            globalGravController = FindObjectOfType<GlobalGravityController>();
            rb = col.gameObject.GetComponent<Rigidbody>();
            globalGravController.gravityDirection = direction;
            rb.freezeRotation = lockRotation;

        }
    }
}
