using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Here");
        if (other.CompareTag("Ball"))
        {
            EventBus.Publish(new ResetShotEvent());
        }
    }
}
