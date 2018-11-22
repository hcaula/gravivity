using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{

    public int fragmentAmount;
    public bool killIt = false;
    private bool isDying = false;
    private int deathCounter = 300;

    private int deathState;
    private float deathTime = 0;
    public float boomDuration = 1;
    public float moveDuration = 1;

    private GameObject[] fragments = null;
    private Vector3[] forces;
    private Vector3 originalPosition;

    public GameObject fragmentPrefab;
	public float fragmentSpeed;

	void Start()
	{
		originalPosition = transform.position;
	}

    void Update()
    {
        deathCounter--;
        if (killIt && !isDying && deathCounter == 0) Die();
        if (isDying) {
            if (deathTime < moveDuration * 0.01)
            {
                deathState = 2;
                isDying = false;
                for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
                {
                    fragments[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0), ForceMode.Acceleration);
                    fragments[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    Destroy(fragments[i]);
                }
                GetComponent<Rigidbody>().position = originalPosition;
                GetComponent<MeshRenderer>().enabled = true;
            }
            else if (deathState == 1 && deathTime < moveDuration) { 
                RedirectFragments(); 
            }

            deathTime -= Time.deltaTime;

        }
    }

    void Die()
    {
        isDying = true;
        deathTime = moveDuration + boomDuration;
        deathState = 1;
		AnimateFragments();
    }

    void RedirectFragments() {
        for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
        {
            Vector3 delta = originalPosition - fragments[i].GetComponent<Rigidbody>().position;
            Vector3 avgSpeed = delta / deathTime;
            Vector3 iniSpeed = fragments[i].GetComponent<Rigidbody>().velocity;
            Vector3 finalSpeed = 2 * avgSpeed - iniSpeed;
            Vector3 deltaSpeed = finalSpeed - iniSpeed;
            Vector3 acceleration = deltaSpeed / deathTime;
            fragments[i].GetComponent<Rigidbody>().AddRelativeForce(acceleration, ForceMode.Acceleration);
        }
    }

	void AnimateFragments()
	{
		GetComponent<MeshRenderer>().enabled = false;

		fragments = new GameObject[fragmentAmount * fragmentAmount * fragmentAmount];
        forces = new Vector3[fragmentAmount * fragmentAmount * fragmentAmount];

        for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
        {
            float y = ((i/(fragmentAmount * fragmentAmount)) - fragmentAmount / 2) + Random.Range(-1.0f/fragmentAmount, 1.0f / fragmentAmount);
            float x = ((i / fragmentAmount) % fragmentAmount - fragmentAmount / 2) + Random.Range(-1.0f / fragmentAmount, 1.0f / fragmentAmount);
			float z = (i % fragmentAmount - fragmentAmount / 2) + Random.Range(-1.0f / fragmentAmount, 1.0f / fragmentAmount);

            GameObject fragment = Instantiate(fragmentPrefab, this.transform);

            forces[i] = new Vector3(x, y, z) * fragmentSpeed;

            fragment.GetComponent<Rigidbody>().AddForce(forces[i]);

			fragments[i] = fragment;
        }
	}
}
