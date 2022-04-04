using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWorldData : MonoBehaviour
{

    Subscription<WorldUnlockedEvent> world_unlock_sub;
    Subscription<BallBoughtEvent> ball_bought_sub;

    static HomeWorldData instance;

    List<int> purchased_balls;
    List<int> unlocked_worlds;

    public Material[] ball_mats;

    int active_ball = 0;
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

        purchased_balls = new List<int>();
        purchased_balls.Add(0);

        unlocked_worlds = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<int> GetUnlockedWorlds()
    {
        return unlocked_worlds;
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

    public Material GetActiveMaterial()
    {
        return ball_mats[active_ball];
    }

    public void SetActiveBall(int index)
    {
        active_ball = index;
    }


    void UnlockWorld(WorldUnlockedEvent e)
    {
        unlocked_worlds.Add(e.num);
    }
}
