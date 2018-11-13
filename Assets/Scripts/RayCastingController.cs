using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingController : MonoBehaviour {

	public GameObject[] targetedPlatforms;
	
	void Update () {
		Vector3 center = GameObject.FindGameObjectsWithTag("Center")[0].gameObject.transform.position;
		Vector3 camera = transform.position;
		Vector3 castDirection = center - camera;
		float distance = Vector3.Distance(camera, center);

		Ray landingRay = new Ray(camera, castDirection);

		RaycastHit hit;
		if (Physics.Raycast(landingRay, out hit, distance))
		{
			GameObject targetHit = hit.collider.gameObject;
			for (int i = 0; i < targetedPlatforms.Length; i++) 
			{
				GameObject platform = targetedPlatforms[i].gameObject;
				bool isOnView = false;
				if (targetHit != null && GameObject.ReferenceEquals(platform, targetHit)) isOnView = true;
				platform.GetComponent<TransparentController>().SetIsOnView(isOnView);
			}
		}
	}
}
