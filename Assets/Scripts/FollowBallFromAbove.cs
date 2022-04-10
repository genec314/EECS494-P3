using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallFromAbove : MonoBehaviour
{
    [SerializeField] Transform ball;

    Transform tf;

    private Vector3 offset;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        offset = tf.position - ball.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tf.position = ball.position + offset;
    }
}
