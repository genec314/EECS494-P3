using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

	public Transform target;
	public Vector3 shot_offset;
	public Vector3 follow_offset;
	public float ease_factor = 0.025f;

	bool following = false;
	Subscription<BallThrownEvent> ball_thrown_sub;
	Subscription<BallReadyEvent> ball_ready_sub;
	Subscription<TeleportEvent> teleport_sub;
	Subscription<LevelStartEvent> start_sub;


	// Use this for initialization
	void Start()
	{
		Application.targetFrameRate = 60;
		UpdateOffsets();
		ball_thrown_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
		teleport_sub = EventBus.Subscribe<TeleportEvent>(OnTeleport);
		ball_ready_sub = EventBus.Subscribe<BallReadyEvent>(OnBallReady);
		start_sub = EventBus.Subscribe<LevelStartEvent>(OnLevelStart);
	}

	void OnDestroy()
	{
		EventBus.Unsubscribe<BallThrownEvent>(ball_thrown_sub);
		EventBus.Unsubscribe<BallReadyEvent>(ball_ready_sub);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(target.transform.position, Vector3.up, -75 * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target.transform.position, Vector3.up, 75 * Time.deltaTime);
        }

		if (!following) target.transform.forward = transform.forward;
		
		UpdateOffsets();
		Follow();
	}

	void LateUpdate()
	{
		// UpdateOffsets();
		// Follow();
	}

	void UpdateOffsets()
	{
		shot_offset = -7.5f * transform.forward + 2.5f * Vector3.up;
		follow_offset = -5.5f * transform.forward + 1.5f * Vector3.up;
	}

	void Follow()
	{
		// if (following) transform.position = Vector3.Lerp(transform.position, target.position + follow_offset, ease_factor);
		// else transform.position = Vector3.Lerp(transform.position, target.position + shot_offset, ease_factor);
		
		// Vector3 velocity = Vector3.zero;

		// if (following) transform.position = Vector3.SmoothDamp(transform.position, target.position + follow_offset, ref velocity, ease_factor);
		// else transform.position = Vector3.SmoothDamp(transform.position, target.position + shot_offset, ref velocity, ease_factor);

		Vector3 desired_position;

		if (following) desired_position = target.position + follow_offset;
		else desired_position = target.position + shot_offset;

		transform.position += (desired_position - transform.position) * ease_factor * Application.targetFrameRate * Time.deltaTime;
	}

	void OnLevelStart(LevelStartEvent e)
	{
		following = false;
	}

	void OnBallThrown(BallThrownEvent e)
    {
		following = true;
    }

	void OnBallReady(BallReadyEvent e)
	{
		following = false;
	}

	void OnTeleport(TeleportEvent e)
    {
		if (e.t1 != null && e.updateCamera)
        {
			Camera.main.transform.position = e.t2.position + follow_offset;
        }
    }
}