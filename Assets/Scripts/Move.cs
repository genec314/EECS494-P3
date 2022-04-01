using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float ready_delay = 2f;
    Rigidbody rb;
    Transform tf;
    float last_thrown_time;
    bool at_rest = true;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<ResetShotEvent> reset_shot_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        last_thrown_time = Time.time - 1;
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(ThrowBall);
        reset_shot_subscription = EventBus.Subscribe<ResetShotEvent>(ResetShot);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallThrownEvent>(thrown_subscription);
    }

    void FixedUpdate()
    {
        if (!at_rest && Time.time > last_thrown_time + 0.5f && rb.velocity.magnitude < 1f)
        {
            at_rest = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            EventBus.Publish<BallAtRestEvent>(new BallAtRestEvent());
            StartCoroutine(WaitThenPublishEvent());
        }
    }

    void ThrowBall(BallThrownEvent e)
    {
        rb.AddForce(tf.transform.forward * e.velocity);
        last_thrown_time = Time.time;
        at_rest = false;
    }

    IEnumerator WaitThenPublishEvent()
    {
        yield return new WaitForSeconds(ready_delay);
        EventBus.Publish<BallReadyEvent>(new BallReadyEvent());
    }

    void ResetShot(ResetShotEvent e)
    {
        if (e.position.x != -999)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            tf.position = e.position;
        }
    }
}
