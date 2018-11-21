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
            if (deathState == 1 && deathTime < moveDuration) RedirectFragments();
            if (deathTime < 0)
            {
                isDying = false;
                for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++) fragments[i].GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
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
            fragments[i].GetComponent<Rigidbody>().AddForce(-forces[i]);
            Vector3 delta = originalPosition - fragments[i].GetComponent<Rigidbody>().position;
            fragments[i].GetComponent<Rigidbody>().AddForce(delta / deathTime, ForceMode.VelocityChange);
        }
        deathState = 2;
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
