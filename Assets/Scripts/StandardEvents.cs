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
    public PinKnockedOverEvent() {}
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

public class LoadTitleEvent
{
    public LoadTitleEvent() {}
}

// Used to tell game control to switch to a new world.
public class LoadWorldEvent
{
    public int world_num;
    public LoadWorldEvent(int _world)
    {
        world_num = _world;
    }
}

// Used to tell game control which level to load. Published by level select menu
public class SelectLevelEvent
{
    public int level_num;
    public SelectLevelEvent(int _level)
    {
        level_num = _level;
    }
}

// Used to tell a level to load itself. Published by GameControl
public class LoadLevelEvent
{
    public int level_num;
    public int world_num;
    public LoadLevelEvent(int _level, int _world)
    {
        level_num = _level;
        world_num = _world;
    }
}

// Used to forcibly restart the current level, used by GameControl. Published by pause menu
public class RestartLevelEvent
{
    public RestartLevelEvent() {}
}

public class LoadLevelSelectEvent
{
    public LoadLevelSelectEvent() {}
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

// Used to notify that the level has ended, used by GameControl. Published by level
public class LevelEndEvent
{
    public bool complete;

    public LevelEndEvent(bool _complete)
    {
        complete = _complete;
    }
}

// Used to set all initial state needed when a level begins. Published by level
public class LevelStartEvent
{
    public HoleData level;

    public LevelStartEvent(HoleData _level)
    {
        level = _level;
    }
}

public class TutorialStrikeEvent
{
    public TutorialStrikeEvent() {}

}

public class TeleportEvent
{
    public Transform t1 = null;
    public Transform t2 = null;

    public bool updateCamera;

    public TeleportEvent() {}

    public TeleportEvent(Transform _t1, Transform _t2)
    {
        t1 = _t1;
        t2 = _t2;
        updateCamera = true;
    }

    public TeleportEvent(Transform _t1, Transform _t2, bool _updateCamera)
    {
        t1 = _t1;
        t2 = _t2;
        updateCamera = _updateCamera;
    }
}

public class ResetShotEvent {

    public Vector3 position = new Vector3(-999, -999, -999);

    public ResetShotEvent() {}

    public ResetShotEvent(Vector3 _position)
    {
        position = _position;
    }
}

// Used to reset pins. Published by holes and home world
public class ResetPinsEvent
{
    public ResetPinsEvent() {}
}

// Used for toast and other stuff that may need to happen in between a level ending and the current level restarting. Published only by GameControl
public class LevelFailedEvent
{
    public LevelFailedEvent() {}
}

// Used for toast and other stuff that may need to happen in between a level ending and the next level starting. Published only by GameControl
public class LevelCompleteEvent
{
    public LevelCompleteEvent() {}
}

// Used for toast when a world is first completed. TODO: add this
public class WorldCompleteEvent
{
    public WorldCompleteEvent() {}
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
    public HomeWorldExitEvent() {}
}

public class StopBarEvent
{
    public StopBarEvent() {}
}

public class WorldUnlockedEvent
{
    public int num;
    public WorldUnlockedEvent(int _num)
    {
        num = _num;
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

}

public class GainPinsEvent
{
    public int num;

    public GainPinsEvent(int _num)
    {
        num = _num;
    }
}

public class SceneTransitionEvent
{
    public SceneTransitionEvent() {}
}