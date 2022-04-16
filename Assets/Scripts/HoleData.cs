using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// waiting on gene to publish data when shot ended

public class HoleData : MonoBehaviour
{
    [SerializeField] Vector3 initialBallPos;
    [SerializeField] Vector3 initialCameraPos;
    [SerializeField] Vector3 initalCameraRotation;
    [SerializeField] int numPins;
    [SerializeField] int numShots;
    int shots_taken = 0;
    int pins_down = 0;
    public int worldNumber;
    public int levelNumber;
    Subscription<PinKnockedOverEvent> pin_subscription;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<ResetShotEvent> reset_shot_subscription;
    Subscription<BallReadyEvent> ball_ready_subscription;
    Subscription<LoadLevelEvent> load_subscription;
    public bool current_hole = false;
    public bool canBallSplit = false;

    // Start is called before the first frame update
    void Awake()
    {
        pin_subscription = EventBus.Subscribe<PinKnockedOverEvent>(PinDown);
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(IncreaseShots);
        reset_shot_subscription = EventBus.Subscribe<ResetShotEvent>(ResetShot);
        ball_ready_subscription = EventBus.Subscribe<BallReadyEvent>(BallReady);
        load_subscription = EventBus.Subscribe<LoadLevelEvent>(OnLoadLevel);
    }

    private void PinDown(PinKnockedOverEvent p)
    {
        if (current_hole) pins_down++;
    }

    private void BallReady(BallReadyEvent e)
    {
        if (current_hole)
        {
            if (pins_down == numPins)
            {
                EventBus.Publish<LevelEndEvent>(new LevelEndEvent(true));
            }
            else if (shots_taken == numShots)
            {
                EventBus.Publish<LevelEndEvent>(new LevelEndEvent(false));
            }
            else if (shots_taken > 0)
            {
                EventBus.Publish<ReadySoundEvent>(new ReadySoundEvent());
            }
        }
    }

    void OnLoadLevel(LoadLevelEvent e)
    {
        if (e.world_num == worldNumber && e.level_num == levelNumber) current_hole = true;
        else current_hole = false;

        if (current_hole)
        {
            pins_down = 0;
            shots_taken = 0;
            EventBus.Publish<LevelStartEvent>(new LevelStartEvent(this));
        }
    }

    private void IncreaseShots(BallThrownEvent b)
    {
        if (current_hole)
        {
            shots_taken++;
        }
    }

    public Vector3 GetInitialBallPosition()
    {
        return initialBallPos;
    }

    public Vector3 GetInitialCameraPosition()
    {
        return initialCameraPos;
    }

    public Vector3 GetInitialCameraRotation()
    {
        return initalCameraRotation;
    }

    public int GetNumShots()
    {
        return numShots;
    }

    public int GetShotsTaken()
    {
        return shots_taken;
    }

    public int GetPinsRemaining()
    {
        return pins_down;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    private void ResetShot(ResetShotEvent e)
    {
        if (current_hole && e.position.x == -999)
        {
            EventBus.Publish(new ResetShotEvent(initialBallPos));
        }
    }
}
