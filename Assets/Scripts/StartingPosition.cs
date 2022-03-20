using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    [SerializeField] Transform target;
    Transform tf;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        tf.position = target.position + new Vector3(0, 6.5f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
