using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingController : MonoBehaviour
{
    public GameObject[] opaquableWalls; /* Walls that can be made transparent */
    private GameObject[] centers;

    void Update()
    {
        foreach (GameObject target in opaquableWalls)
        {
            TransparentController tc = target.GetComponent<TransparentController>();

            bool hit = ObjectIsOnView(target);
            if (hit && !tc.GetIsOnView()) tc.SetIsOnView(true);
            else if (!hit && tc.GetIsOnView()) tc.SetIsOnView(false);
        }
    }

    bool ObjectIsOnView(GameObject obj)
    {
        Vector3 camera = transform.position;
        centers = GameObject.FindGameObjectsWithTag("Center");
        bool haveHit = false;
        foreach (GameObject centerObject in centers)
        {
            Vector3 centerPosition = centerObject.transform.position;
            Vector3 castDirection = centerPosition - camera;
            float distance = Vector3.Distance(camera, centerPosition);
            Ray landingRay = new Ray(camera, castDirection);

            RaycastHit hit;
            if (Physics.Raycast(landingRay, out hit, distance))
            {
                GameObject targetHit = hit.collider.gameObject;
                if (targetHit.GetInstanceID() == obj.GetInstanceID()) haveHit = true;
            }
        }
        return haveHit;
    }

    // TransparentController tc = targetHit.GetComponent<TransparentController>();
    //             if (!tc)
    //             {
    //                 print("Object can't be made transparent because it doesn't have a TransparentController component");
    //             }
    //             else if (!tc.GetIsOnView())
    //             {
    //                 tc.SetIsOnView(true);
    //                 transparentPlatforms.Add(targetHit);
    //                 // print("hi");
    //             }
}
