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

public class BallReadyEvent
{
    public BallReadyEvent() {}
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

public class LoadIntroEvent
{
    public LoadIntroEvent() {}
}

public class LoadNextLevelEvent
{
    public LoadNextLevelEvent()
    {

    }
}

public class LoadLevelEvent
{
    int level_num;
    int world_num;
    public LoadLevelEvent(int _level, int _world)
    {
        level_num = _level;
        world_num = _world;
    }
}

public class UpdateLevelDataEvent
{
    int level_num;
    int world_num;
    int pins_down;
    bool complete;

    public UpdateLevelDataEvent(int _level, int _world, int _pins, bool _complete)
    {
        level_num = _level;
        world_num = _world;
        pins_down = _pins;
        complete = _complete;
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

<<<<<<< HEAD
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

public class ResetShotEvent {

    public Vector3 position = new Vector3(-999, -999, -999);

    public ResetShotEvent()
    {

    }

    public ResetShotEvent(Vector3 _position)
    {
        position = _position;
    }
}

public class ResetPinsEvent
{
    public ResetPinsEvent()
    {

    }
}

public class HomeWorldSelectEvent
{
    public string where;

    public HomeWorldSelectEvent(string _where)
    {
        where = _where;
    }
}

public class HomeWorldExitEvent
{
    public HomeWorldExitEvent()
=======
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
>>>>>>> gene_alpha
    {

    }
}

<<<<<<< HEAD
public class WorldUnlockedEvent
{
    public int num;
    public int cost;
    public WorldUnlockedEvent(int _num, int _cost)
    {
        num = _num;
        cost = _cost;
    }
}

public class BallBoughtEvent
{
    public int num;
    public int cost;
    public BallBoughtEvent(int _num, int _cost)
    {
        num = _num;
        cost = _cost;
    }

=======
public class ResetShotEvent {

    public Vector3 position = new Vector3(-999, -999, -999);

    public ResetShotEvent()
    {

    }

    public ResetShotEvent(Vector3 _position)
    {
        position = _position;
    }
>>>>>>> gene_alpha
}