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

public class BallAtRestEvent
{
    public BallAtRestEvent() {}
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

public class PowerBarWindupEvent
{
    public PowerBarWindupEvent() {}
}

public class PowerBarReleaseEvent
{
    public PowerBarReleaseEvent() {}
}

public class LoadNextLevelEvent
{
    public LoadNextLevelEvent()
    {

    }
}

public class ReloadLevelEvent
{
    public ReloadLevelEvent() {}
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

public class RanOutOfLivesEvent
{
   

    public RanOutOfLivesEvent()
    {
       
    }
}

public class EndHoleEvent
{
    public HoleData currentHole;

    public EndHoleEvent(HoleData _currentHole)
    {
        currentHole = _currentHole;
    }
}

public class NewHoleEvent
{
    public HoleData nextHole;

    public NewHoleEvent(HoleData _nextHole)
    {
        nextHole = _nextHole;
    }
}

public class TutorialStrikeEvent
{
    public TutorialStrikeEvent() { }

}

public class TeleportEvent
{
    public Transform t1 = null;
    public Transform t2 = null;

    public TeleportEvent()
    {

    }

    public TeleportEvent(Transform _t1, Transform _t2)
    {
        t1 = _t1;
        t2 = _t2;
    }
}

public class UpdateCameraRotationEvent
{
    public UpdateCameraRotationEvent()
    {

    }
}