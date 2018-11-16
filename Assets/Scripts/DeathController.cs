using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{

    public int fragmentAmount;
    public bool killIt = false;
    private bool isDying = false;
	private Vector3 originalPosition;

    public GameObject fragmentPrefab;
	public float fragmentSpeed;

	void Start()
	{
		originalPosition = transform.position;
	}

    void Update()
    {
        if (killIt && !isDying) Die();
    }

    void Die()
    {
        isDying = true;
		AnimateFragments();
    }

	void AnimateFragments()
	{
		GetComponent<MeshRenderer>().enabled = false;
		int min = -10, max = 10;

		GameObject[] fragments = new GameObject[fragmentAmount];

		for (int i = 0; i < fragmentAmount; i++)
        {
			float y = Random.Range(min, max);
			float x = Random.Range(min, max);
			float z = Random.Range(min, max);

            GameObject fragment = Instantiate(fragmentPrefab, this.transform);

			fragment.GetComponent<Rigidbody>().AddForce(new Vector3(x, y ,z) * fragmentSpeed);

			fragments[i] = fragment;
        }
	}
}
