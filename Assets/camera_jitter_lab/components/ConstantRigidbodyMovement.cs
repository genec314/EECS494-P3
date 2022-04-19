using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRigidbodyMovement : MonoBehaviour
{

    public Vector3 movement = Vector3.zero;
    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent <Rigidbody> ();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        rb.velocity = movement;
    }
}
