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
