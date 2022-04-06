using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallPosition : MonoBehaviour
{
    Transform tf;
    Rigidbody rb;

    private float time;

    Subscription<LevelStartEvent> start_subscription;
    Subscription<ResetShotEvent> reset_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();

        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
        reset_subscription = EventBus.Subscribe<ResetShotEvent>(ResetShot);
    }

    private void StartLevel(LevelStartEvent e)
    {
        tf.position = e.level.GetInitialBallPosition();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        time = Time.time;
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
