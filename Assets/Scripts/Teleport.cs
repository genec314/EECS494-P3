using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Transform tf;

    public Transform to;

    private bool justTeleported = false;

    Subscription<TeleportEvent> teleport_sub;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();

        teleport_sub = EventBus.Subscribe<TeleportEvent>(OnTeleport);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !justTeleported)
        {
            justTeleported = true;

            EventBus.Publish(new TeleportEvent(tf, to));

            other.gameObject.GetComponent<Transform>().position = to.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        justTeleported = false;

        // Know for a fact ball's position is at new teleporter now
        EventBus.Publish(new UpdateCameraRotationEvent());
    }

    private void OnTeleport(TeleportEvent e)
    {
        if (e.t1 != null)
        {
            justTeleported = true;
            return;
        }

        justTeleported = false;
    }
}
