using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTarget : MonoBehaviour
{
    public Transform target;
    Transform tf;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(target.transform.position, Vector3.up, -75 * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target.transform.position, Vector3.up, 75 * Time.deltaTime);
        }

        target.transform.forward = tf.transform.forward;
    }
}
