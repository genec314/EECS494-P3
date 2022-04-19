using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{

    public Vector3 movement = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
		
    }
	
    // Update is called once per frame
    void Update ()
    {
        transform.position += movement * Time.deltaTime;
    }
}
