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
	Subscription<BallAtRestEvent> ball_rest_sub;

	// Use this for initialization
	void Start()
	{
		UpdateOffsets();
		ball_thrown_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
		ball_rest_sub = EventBus.Subscribe<BallAtRestEvent>(OnBallAtRest);
	}

	void OnDestroy()
	{
		EventBus.Unsubscribe<BallThrownEvent>(ball_thrown_sub);
		EventBus.Unsubscribe<BallAtRestEvent>(ball_rest_sub);
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
		shot_offset = -10f * transform.forward + 3f * Vector3.up;
		follow_offset = -2f * transform.forward + 0.4f * Vector3.up;
	}

	void OnBallThrown(BallThrownEvent e)
    {
		following = true;
    }

	void OnBallAtRest(BallAtRestEvent e)
	{
		following = false;
	}
}