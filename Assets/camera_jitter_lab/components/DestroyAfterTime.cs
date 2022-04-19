using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time_to_live = 5.0f;

    float time_of_destruction = 0.0f;

    // Use this for initialization
    void Start ()
    {
        time_of_destruction = Time.time + time_to_live;	
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (Time.time >= time_of_destruction) {
            Destroy (gameObject);
        }
    }
}
