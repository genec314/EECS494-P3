using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFollowTarget : MonoBehaviour
{

	public Transform ball;
	public Transform pins;

	public Vector3 offset;
	public float stopDist;

	public float ease_factor = 0.1f;

	bool following = false;
	Subscription<BallThrownEvent> ball_thrown_sub;
	// Use this for initialization
	void Start()
	{
		ball_thrown_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 diff = ball.position - pins.position;
		if(Mathf.Abs(diff.z) < stopDist)
        {
			following = false;
        }
		Vector3 restPos = -transform.forward * 3;
		restPos += transform.up;
		if (following)
			transform.position = Vector3.Lerp(transform.position, ball.position + restPos, ease_factor);
	}

	void OnBallThrown(BallThrownEvent e)
	{
		following = true;
	}


}