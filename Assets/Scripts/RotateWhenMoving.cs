using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWhenMoving : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal") * GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;


        transform.Rotate(Vector3.forward, direction);
    }
}
