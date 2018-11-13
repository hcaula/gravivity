using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingController : MonoBehaviour
{
    public GameObject[] targetedPlatforms;

    void Update()
    {
		List<GameObject> transparentPlatforms = new List<GameObject>();
        Vector3 camera = transform.position;
        GameObject[] centers = GameObject.FindGameObjectsWithTag("Center");
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
                TransparentController tc = targetHit.GetComponent<TransparentController>();
                if (!tc)
                {
                    print("Object can't be made transparent because it doesn't have a TransparentController component");
                }
                else if (!tc.GetIsOnView())
                {
                    tc.SetIsOnView(true);
                    transparentPlatforms.Add(targetHit);
                }
            }
        }

        foreach (GameObject target in targetedPlatforms)
        {
            transparentPlatforms.ForEach(tp =>
            {
                if (!GameObject.ReferenceEquals(target, tp))
                {
                    TransparentController tc = target.GetComponent<TransparentController>();
                    if (!tc) print("Object can't be made opaque because it doesn't have a TransparentController component");
                    else if (tc.GetIsOnView()) tc.SetIsOnView(true);
                }
            });
        }
    }
}
