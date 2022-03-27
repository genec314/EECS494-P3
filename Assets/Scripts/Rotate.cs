using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float degreesPerFrame;

    Transform tf;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.Rotate(0, degreesPerFrame * Time.deltaTime, 0);
    }
}
