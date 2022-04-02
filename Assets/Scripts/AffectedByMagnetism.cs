using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByMagnetism : MonoBehaviour
{
    [SerializeField] float charge = 0f;

    Subscription<ElectrostaticForceEvent> force_subscription;

    Rigidbody rb;
    Transform tf;

    // Start is called before the first frame update
    void Awake()
    {
        force_subscription = EventBus.Subscribe<ElectrostaticForceEvent>(ElectrostaticEffect);
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ElectrostaticEffect(ElectrostaticForceEvent e)
    {
        if (Vector3.Magnitude(rb.velocity) <= 0.5 && e.charge >= 0)
        {
            return;
        }

        float eCharge = e.charge;
        Vector3 ePosition = e.position;

        Vector3 distanceVector = ePosition - tf.position;

        Vector3 directionVector = distanceVector.normalized;

        float distance = Vector3.Magnitude(distanceVector);

        rb.AddForce((-directionVector * charge * eCharge) / (distance * distance));
    }
}
