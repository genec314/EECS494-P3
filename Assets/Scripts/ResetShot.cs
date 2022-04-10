using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && other.gameObject.name == "ElectricBall")
        {
            EventBus.Publish(new ResetShotEvent());
        }
    }
}
