using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrownEvent
{
    public float velocity;
    public BallThrownEvent(float _velocity)
    {
        velocity = _velocity;
    }

    public override string ToString()
    {
        return "ball thrown with velocity " + velocity;
    }
}

public class PinKnockedOverEvent
{
    public PinKnockedOverEvent()
    {

    }

    public override string ToString()
    {
        return "pin knocked over";
    }
}

public class LoadNextLevelEvent
{
    public LoadNextLevelEvent()
    {

    }
}

public class ElectrostaticForceEvent
{
    public float charge;
    public Vector3 position;

    public ElectrostaticForceEvent(float _charge, Vector3 _position)
    {
        charge = _charge;
        position = _position;
    }
}
