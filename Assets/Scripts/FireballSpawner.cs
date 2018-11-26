using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour {

    public Vector3 dir;
    public GameObject fireball;
    public float speed = 10.0f;
    public float spawnDelay = 1.0f;
    private float lastSpawn = 0;
    private Transform marker;
    private Vector3 pos;


    // Use this for initialization
    void Start()
    {
        marker = transform.GetChild(0);
        pos = transform.position;
        dir = (marker.position - pos).normalized;
        SpawnFireball();
    }

    void SpawnFireball()
    {
        Debug.DrawLine(pos, pos + dir * 100, Color.red, Mathf.Infinity);
        GameObject fb = Instantiate(fireball, marker);
        Rigidbody rb = fb.GetComponent<Rigidbody>();
        rb.velocity.Set(dir.x * speed, dir.y * speed, dir.z * speed);
        Debug.Log("firebal spawned. Pos "+ pos +  "Dir " + dir);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastSpawn + spawnDelay)
        {
            SpawnFireball();
            lastSpawn = Time.time;
        }

    }
}
