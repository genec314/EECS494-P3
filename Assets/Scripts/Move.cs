using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float ready_delay = 1.5f;
    Rigidbody rb;
    Transform tf;
    float last_thrown_time;
    bool at_rest = true;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<LevelStartEvent> start_sub;
    private bool inRepelField = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        last_thrown_time = Time.time - 1;
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(ThrowBall);
        start_sub = EventBus.Subscribe<LevelStartEvent>(CancelAtLevelStart);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallThrownEvent>(thrown_subscription);
    }

    void FixedUpdate()
    {
        // ball will stop if moving at a speed of less than 1 if the throw was more than half a second ago and is not in a repelling field
        if (!at_rest && Time.time > last_thrown_time + 0.5f && rb.velocity.magnitude < 1f && !inRepelField)
        {
            at_rest = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            EventBus.Publish<BallAtRestEvent>(new BallAtRestEvent());
            StartCoroutine(WaitThenPublishEvent());
        }
    }

    void CancelAtLevelStart(LevelStartEvent e)
    {
        StopAllCoroutines();
        at_rest = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            inRepelField = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            inRepelField = false;
        }
    }
}
