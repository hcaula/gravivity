using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    public PortalController exitPortal;
    public Vector3 direction;
    private float minimumDistance = 1.0f;
    private float exitSpeed = 3.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb = col.gameObject.GetComponent<Rigidbody>();
            rb.velocity = direction * exitSpeed;
            rb.velocity = new Vector3(0,0,0);
        }
    }
}
