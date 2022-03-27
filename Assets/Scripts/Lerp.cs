using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    Transform tf;

    Vector3 start;
    public Vector3 to;

    bool way = true;

    public float time;
    private float initial_time;
    private float progress = 0;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        start = tf.position;
        initial_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        progress = (Time.time - initial_time) / time;

        if (progress >= 1)
        {
            way = !way;
            progress = 0;
            initial_time = Time.time;
        }

        if (way)
        {
            tf.position = Vector3.Lerp(start, to, progress);
        } else
        {
            tf.position = Vector3.Lerp(to, start, progress);
        }
    }
}
