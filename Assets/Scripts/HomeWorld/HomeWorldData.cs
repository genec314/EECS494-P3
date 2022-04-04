using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWorldData : MonoBehaviour
{

    Subscription<WorldUnlockedEvent> world_unlock_sub;
    Subscription<BallBoughtEvent> ball_bought_sub;

    static HomeWorldData instance;

    bool[] activeLanes;
    List<int> purchased_balls;
    List<int> unlocked_worlds;

    int active_ball;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);

        world_unlock_sub = EventBus.Subscribe<WorldUnlockedEvent>(UnlockWorld);
        ball_bought_sub = EventBus.Subscribe<BallBoughtEvent>(BallBought);

        activeLanes = new bool[3]{ false, false, false};

        purchased_balls = new List<int>();
        purchased_balls.Add(0);

        unlocked_worlds = new List<int>();
        unlocked_worlds.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool[] GetActiveLanes()
    {
        return activeLanes;
    }

    public List<int> GetPurchasedBalls()
    {
        return purchased_balls;
    }

    void BallBought(BallBoughtEvent e)
    {
        purchased_balls.Add(e.num);
    }

    public int GetActiveBall()
    {
        return active_ball;
    }

    public void SetActiveBall(int index)
    {
        active_ball = index;
    }

    public List<int> GetUnlockedWorlds()
    {
        return unlocked_worlds;
    }

    void UnlockWorld(WorldUnlockedEvent e)
    {
        unlocked_worlds.Add(e.num);
    }
}
