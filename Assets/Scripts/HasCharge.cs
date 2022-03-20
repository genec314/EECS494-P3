using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasCharge : MonoBehaviour
{
    [SerializeField] float charge = 0f;

    Transform tf;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
    }

    public void BroadcastCharge()
    {
        EventBus.Publish(new ElectrostaticForceEvent(charge, tf.position));
    }
}
