using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallPosition : MonoBehaviour
{
    Transform tf;
    Rigidbody rb;
    TrailRenderer tr;

    private float time;

    Subscription<NewHoleEvent> new_hole_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        tr = this.GetComponent<TrailRenderer>();

        new_hole_subscription = EventBus.Subscribe<NewHoleEvent>(NewHole);
    }

    private void NewHole(NewHoleEvent e)
    {
        tr.time = 0;
        tf.position = e.nextHole.GetInitialBallPosition();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        time = Time.time;
        StartCoroutine(TwoSeconds());
    }

    IEnumerator TwoSeconds()
    {
        while (Time.time - time < 2f)
        {
            tr.time = Time.time - time;
            yield return null;
        }

        tr.time = 2;
    }

}
