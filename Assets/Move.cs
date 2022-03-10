using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = Input.GetAxis("Horizontal") * 3;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            velocity.z = 7.4f;
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            velocity.z = -7.4f;

        }

        rb.velocity = velocity;
    }
}
