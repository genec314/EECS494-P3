using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKnockOver : MonoBehaviour
{
    Transform tf;
    bool knockedOver = false;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!knockedOver && tf.up.y < 0.5f)
        {
            knockedOver = true;
            PinKnockedOverEvent knock = new PinKnockedOverEvent();
            EventBus.Publish(knock);
        }
    }
}