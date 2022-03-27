using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

	public Transform target;
	public Vector3 shot_offset;
	public Vector3 follow_offset;
	public float ease_factor = 0.1f;

	bool following = false;
	Subscription<BallThrownEvent> ball_thrown_sub;
	Subscription<BallReadyEvent> ball_ready_sub;

	// Use this for initialization
	void Start()
	{
		UpdateOffsets();
		ball_thrown_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
		ball_ready_sub = EventBus.Subscribe<BallReadyEvent>(OnBallReady);
	}

	void OnDestroy()
	{
		EventBus.Unsubscribe<BallThrownEvent>(ball_thrown_sub);
		EventBus.Unsubscribe<BallReadyEvent>(ball_ready_sub);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		UpdateOffsets();
		if (following) transform.position = Vector3.Lerp(transform.position, target.position + follow_offset, ease_factor);
		else transform.position = Vector3.Lerp(transform.position, target.position + shot_offset, ease_factor);
	}

	void UpdateOffsets()
	{
		shot_offset = -7.5f * transform.forward + 2.5f * Vector3.up;
		follow_offset = -4.5f * transform.forward + 1.5f * Vector3.up;
	}

	void OnBallThrown(BallThrownEvent e)
    {
		following = true;
    }

	void OnBallReady(BallReadyEvent e)
	{
		following = false;
	}
}