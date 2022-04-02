using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPin : MonoBehaviour
{
    Subscription<ResetPinsEvent> reset_pin_sub;

    Vector3 startPos;
    Quaternion startRot;
    Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        reset_pin_sub = EventBus.Subscribe<ResetPinsEvent>(_OnResetPins);
        startPos = transform.localPosition;
        startRot = transform.localRotation;
        startColor = GetComponent<MeshRenderer>().material.color;
    }

    void _OnResetPins(ResetPinsEvent e)
    {
        transform.localPosition = startPos;
        transform.localRotation = startRot;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        GetComponent<MeshRenderer>().material.color = startColor;
        GetComponent<MeshCollider>().enabled = true;
    }
}
