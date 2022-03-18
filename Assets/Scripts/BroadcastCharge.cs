using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastCharge : MonoBehaviour
{
    HasCharge hasCharge;

    // Start is called before the first frame update
    void Awake()
    {
        hasCharge = this.GetComponentInParent<HasCharge>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hasCharge.BroadcastCharge();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hasCharge.BroadcastCharge();
        }
    }
}
