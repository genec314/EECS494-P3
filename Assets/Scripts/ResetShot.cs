using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Here");
        if (other.CompareTag("Ball"))
        {
            EventBus.Publish(new ResetShotEvent());
        }
    }
}
