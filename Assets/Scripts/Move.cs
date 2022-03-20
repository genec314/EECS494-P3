using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;
    float last_thrown_time;
    Subscription<BallThrownEvent> thrown_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        last_thrown_time = Time.time - 1;
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(ThrowBall);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallThrownEvent>(thrown_subscription);
    }

    void FixedUpdate()
    {
        if (!RollBall.canMove && Time.time > last_thrown_time + 0.5f && rb.velocity.magnitude < 1f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            EventBus.Publish<BallAtRestEvent>(new BallAtRestEvent());
        }
    }

    void ThrowBall(BallThrownEvent e)
    {
        rb.AddForce(tf.transform.forward * e.velocity);
        last_thrown_time = Time.time;
    }
}
