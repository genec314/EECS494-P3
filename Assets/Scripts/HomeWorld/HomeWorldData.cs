using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWorldData : MonoBehaviour
{

    static HomeWorldData instance;

    bool[] activeLanes;
    List<int> purchased_balls;
    List<int> unlocked_worlds;

    int active_ball;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);

        activeLanes = new bool[3]{ false, false, false};

        purchased_balls = new List<int>();
        purchased_balls.Add(0);

        unlocked_worlds = new List<int>();
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

    public void PurchaseBall(int index)
    {
        purchased_balls.Add(index);
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

    public void UnlockWorld(int index)
    {
        unlocked_worlds.Add(index);
    }
}
