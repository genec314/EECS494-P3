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
    public int id;
    public PinKnockedOverEvent(int _id)
    {
        id = _id;
    }

    public override string ToString()
    {
        return "pin #" + id + " knocked over";
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
