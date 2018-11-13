using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingController : MonoBehaviour
{
    public GameObject[] opaquableWalls; /* Walls that can be made transparent */

    private GameObject[] centerObjects; /* The objects that the camera will point to raycast */

    void Start()
    {
        /* We can initialize the center objects here since no new center objects are instantiated */
        centerObjects = GameObject.FindGameObjectsWithTag("Center");
    }

    void Update()
    {
        /* Calculates the rays directed towards center stage */
        Ray[] castableRays = GetCastableRays();

        foreach (GameObject target in opaquableWalls)
        {
            TransparentController tc = target.GetComponent<TransparentController>();

            /* Check if any raycast has hit the object */
            bool hit = AnyRaycastHasHit(target, castableRays);

            /* If the object already was blocking the view */
            bool isBlocking = tc.GetIsOnView();

            /* If a raycast has hit the object and it was opaque, make it transparent */
            if (hit && !isBlocking) tc.SetIsOnView(true);

            /* If no raycast has hit the object, but it was transparent, make it opaque */
            else if (!hit && isBlocking) tc.SetIsOnView(false);
        }
    }

    Ray[] GetCastableRays()
    {
        /* Get camera position */
        Vector3 camera = transform.position;

        Ray[] castableRays = new Ray[4];
        int i = 0;
        foreach (GameObject centerObject in centerObjects)
        {
            /* Since the center position can change, we need to check every frame */
            Vector3 centerPosition = centerObject.transform.position;

            /* A vector from the camera pointing towards a center object */
            Vector3 castDirection = centerPosition - camera;

            Ray landingRay = new Ray(camera, castDirection);
            castableRays[i] = landingRay;
            i++;
        }

        return castableRays;
    }

    bool AnyRaycastHasHit(GameObject obj, Ray[] castableRays)
    {
        bool haveHit = false;
        foreach (Ray castableRay in castableRays)
        {
            RaycastHit hit;
            if (Physics.Raycast(castableRay, out hit))
            {
                GameObject targetHit = hit.collider.gameObject;
                if (targetHit.GetInstanceID() == obj.GetInstanceID()) haveHit = true;
            }
        }
        return haveHit;
    }
}
