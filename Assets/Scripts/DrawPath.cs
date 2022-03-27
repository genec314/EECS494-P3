using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public float length = 1.25f;
    public LayerMask pinsLayer;
    LineRenderer line;
    Transform tf;
    Subscription<BallReadyEvent> ready_sub;
    Subscription<BallThrownEvent> throw_sub;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        tf = GetComponent<Transform>();

        ready_sub = EventBus.Subscribe<BallReadyEvent>(EnableAtReady);
        throw_sub = EventBus.Subscribe<BallThrownEvent>(DisableAtThrow);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        Vector3 globalForward = Vector3.ProjectOnPlane(tf.forward, Vector3.up);
        hits = Physics.RaycastAll(tf.position, globalForward, length, ~pinsLayer);

        List<Vector3> positions = new List<Vector3>();
        positions.Add(transform.position);

        if (hits.Length == 0)
        {
            positions.Add(transform.position + length * globalForward);
        }
        else
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                positions.Add(hit.point);
            }
        }
        
        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());
    }

    void EnableAtReady(BallReadyEvent e)
    {
        line.enabled = true;
    }

    void DisableAtThrow(BallThrownEvent e)
    {
        line.enabled = false;
    }
}
