using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformController : MonoBehaviour
{

	#region Public attributes
    public Vector3 minPosition, maxPosition;
    public int speed;
	#endregion

	#region Private attributes
    private ButtonInteractionController bic;
	private Vector3 currentTarget;
	#endregion

    void Start()
    {
        bic = GetComponent<ButtonInteractionController>();
		currentTarget = maxPosition;
    }

    void Update()
    {
		/* If this platform is linked to a button, check if it's pressed before moving */
		/* If it's not linked, move anyways */
        if ((bic && bic.GetIsActive()) || !bic) Move();
    }

    void Move()
    {
		if (currentTarget == transform.position)
		{
			if (currentTarget == maxPosition) currentTarget = minPosition;
			else currentTarget = maxPosition;
		}

        float step = speed * Time.deltaTime;
		Vector3 movement = Vector3.MoveTowards(transform.position, currentTarget, step);	
		
        transform.position = movement;
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Player")
        {
            Transform player = col.gameObject.transform;
            player.parent = this.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision col) 
    {
        if (col.gameObject.tag == "Player")
        {
            Transform player = col.gameObject.transform;
            player.parent = null;
        }
    }

}
