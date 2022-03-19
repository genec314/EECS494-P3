using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallPosition : MonoBehaviour
{
    Transform tf;
    Rigidbody rb;

    Subscription<NewHoleEvent> new_hole_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();

        new_hole_subscription = EventBus.Subscribe<NewHoleEvent>(NewHole);
    }

    private void NewHole(NewHoleEvent e)
    {
        tf.position = e.nextHole.GetInitialBallPosition();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

}
