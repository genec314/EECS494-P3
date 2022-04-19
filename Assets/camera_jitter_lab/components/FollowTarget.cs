using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget2 : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = Vector3.zero;
    public float lerp_factor = 0.1f;

    public bool use_fixed_update = false;
    public bool use_deltatime = false;


    // Use this for initialization
    void Start ()
    {
        Application.targetFrameRate = 60;
    }
	
    // Update is called once per frame
    void Update ()
    {
        _ReportMode ();

        if (!use_fixed_update)
            DoUpdate ();
    }

    void FixedUpdate ()
    {
        if (use_fixed_update)
            DoUpdate ();
    }

    void DoUpdate ()
    {
        if (target == null)
            return;

        Vector3 desired_position = target.position + offset;

        if (use_deltatime)
            transform.position += (desired_position - transform.position) * lerp_factor * Application.targetFrameRate * Time.deltaTime;
        else {
            transform.position += (desired_position - transform.position) * lerp_factor;
        }
    }

    void _ReportMode ()
    {
        string mode_report_string = "Camera Movement - FixedUpdate: " + use_fixed_update.ToString () + ", Use DeltaTime: " + use_deltatime.ToString ();
        CameraModeUI.SetText (mode_report_string);
    }
}
