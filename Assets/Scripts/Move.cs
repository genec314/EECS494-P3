using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;
    Subscription<BallThrownEvent> thrown_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(ThrowBall);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallThrownEvent>(thrown_subscription);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ThrowBall(BallThrownEvent e)
    {
        // rb.AddForce(tf.transform.forward * e.velocity);
    }
}
