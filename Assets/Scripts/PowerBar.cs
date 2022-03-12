using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{

    public float top;
    public float bot;
    public float velocity;

    bool goingUp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tf = transform.localPosition;

        if (goingUp)
            tf.y += velocity;
        
        else
            tf.y -= velocity;

        transform.localPosition = tf;

        if (tf.y >= top)
            goingUp = false;

        else if (tf.y <= bot)
            goingUp = true;

    }

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
        goingUp = true;
    }
}
