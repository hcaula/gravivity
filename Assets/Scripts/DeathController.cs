using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{

    public int fragmentAmount = 3;
    public bool killIt = false;
    private bool isDying = false;
    private int deathCounter = 300;

    private int deathState;
    private float deathTime = 0;
    public float boomDuration = 1;
    public float moveDuration = 1;
    public float clearDuration = 0.2f;

    public float boomDrag = 3;

    private GameObject[] fragments = null;
    private Vector3 originalPosition;

    public GameObject fragmentPrefab;
	public float fragmentSpeed = 8;

	void Start()
	{
		originalPosition = transform.position;
        fragments = new GameObject[fragmentAmount * fragmentAmount * fragmentAmount];
    }

    void Update()
    {
        deathCounter--;
        if (killIt && !isDying && deathCounter == 0) Die();
        if (isDying) {
            if (deathTime < 0)
            {
                for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
                    Destroy(fragments[i]);

                GetComponent<MeshRenderer>().enabled = true;
                GetComponent<Rigidbody>().position = originalPosition;

                isDying = false;
            }
            else if (deathTime < clearDuration)
            {
                for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
                {
                    fragments[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0), ForceMode.Acceleration);
                    fragments[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                }
            }
            else if (deathTime < moveDuration + clearDuration) 
            { 
                RedirectFragments(); 
            }
            else {
                for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
                {
                   
                }
            }

            deathTime -= Time.deltaTime;
        }
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;
        deathTime = moveDuration + boomDuration + clearDuration;
        deathState = 1;
		AnimateFragments();
    }

    void RedirectFragments() {
        for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
        {
            float remMoveTime = deathTime - clearDuration + moveDuration * 0.01f;
            Vector3 delta = originalPosition - fragments[i].GetComponent<Rigidbody>().position;
            Vector3 avgSpeed = delta / remMoveTime;
            Vector3 iniSpeed = fragments[i].GetComponent<Rigidbody>().velocity;
            Vector3 finalSpeed = 2 * avgSpeed - iniSpeed;
            Vector3 deltaSpeed = finalSpeed - iniSpeed;
            Vector3 acceleration = deltaSpeed / remMoveTime;
            fragments[i].GetComponent<Rigidbody>().AddRelativeForce(acceleration, ForceMode.Acceleration);
        }
    }

	void AnimateFragments()
	{
		GetComponent<MeshRenderer>().enabled = false;


        for (int i = 0; i < fragmentAmount * fragmentAmount * fragmentAmount; i++)
        {
            float y = ((i/(fragmentAmount * fragmentAmount)) - fragmentAmount / 2) + Random.Range(-1.0f/fragmentAmount, 1.0f / fragmentAmount);
            float x = ((i / fragmentAmount) % fragmentAmount - fragmentAmount / 2) + Random.Range(-1.0f / fragmentAmount, 1.0f / fragmentAmount);
			float z = (i % fragmentAmount - fragmentAmount / 2) + Random.Range(-1.0f / fragmentAmount, 1.0f / fragmentAmount);

            GameObject fragment = Instantiate(fragmentPrefab, this.transform);

            Vector3 vel = new Vector3(x, y, z) * fragmentSpeed;

            fragment.GetComponent<Rigidbody>().AddForce(vel, ForceMode.VelocityChange);
            fragment.GetComponent<Rigidbody>().drag = boomDrag;

            fragments[i] = fragment;
        }
	}
}
