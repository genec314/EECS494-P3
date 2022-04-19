using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryGenerator : MonoBehaviour
{
    public GameObject cloud_prefab;

    public float generation_interval_sec = 1.0f;
    float next_cloud_gen = 0;

    // Use this for initialization
    void Start ()
    {
        next_cloud_gen = Time.time + generation_interval_sec;
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (Time.time >= next_cloud_gen) {
            next_cloud_gen = next_cloud_gen + generation_interval_sec;
            GenerateCloud ();
        }
    }

    void GenerateCloud ()
    {
        Vector3 new_cloud_pos = transform.position + Vector3.right * 70 + Vector3.forward * 50 * UnityEngine.Random.Range (0.5f, 1.0f) + Vector3.up * 30 * UnityEngine.Random.Range (-1.0f, 1.0f);
        GameObject.Instantiate (cloud_prefab, new_cloud_pos, Quaternion.identity);
    }
}
