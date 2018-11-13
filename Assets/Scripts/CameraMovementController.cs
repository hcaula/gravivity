﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{

	#region Private attributes
    private Transform player;
	#endregion

	#region Public attributes
	public bool isFixed = true;
    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;
	#endregion

	void Start()
	{
		player = GameObject.Find("Cubey").gameObject.transform;
	}

    bool CheckXMargin()
    {
        /* If the distance between the camera and the player in the x axis is greater than the x margin */
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        /* If the distance between the camera and the player in the y axis is greater than the y margin */
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    void FixedUpdate()
    {
		/* Only follow Cubey in long stages */
		if (!isFixed) TrackPlayer();
    }


    void TrackPlayer()
    {
        /* By default the target x and y coordinates of the camera are it's current x and y coordinates */
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        /* If the player has moved beyond the x margin... */
        if (CheckXMargin())

            /* ...the target x coordinate should be a Lerp between the camera's current x position and the player's current x position */
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

        /* If the player has moved beyond the y margin... */
        if (CheckYMargin())
            /*  ...the target y coordinate should be a Lerp between the camera's current y position and the player's current y position */
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

        /* The target x and y coordinates should not be larger than the maximum or smaller than the minimum */
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        /* Set the camera's position to the target position with the same z component */
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
