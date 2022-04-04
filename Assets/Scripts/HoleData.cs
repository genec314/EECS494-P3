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
    [SerializeField] GameObject ball;
    int shots_taken = 0;
    int pins_down = 0;
    [SerializeField] int holeNumber;
    [SerializeField] GameObject nextHole;
    Subscription<PinKnockedOverEvent> pin_subscription;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<ResetShotEvent> reset_shot_subscription;
    Subscription<BallReadyEvent> ball_ready_subscription;
    public bool current_hole = false;
    public bool canBallSplit = false;

    // Start is called before the first frame update
    void Awake()
    {
        pin_subscription = EventBus.Subscribe<PinKnockedOverEvent>(PinDown);
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(IncreaseShots);
        reset_shot_subscription = EventBus.Subscribe<ResetShotEvent>(ResetShot);
        ball_ready_subscription = EventBus.Subscribe<BallReadyEvent>(BallReady);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                StartCoroutine(GoToNextHole());
            }
            else if (shots_taken == numShots)
            {
                StartCoroutine(ResetHole());
            }
        }
    }
    
    IEnumerator GoToNextHole() // Make a more generalizable public function that also calls this
    {
        EventBus.Publish(new EndHoleEvent(this));

        yield return new WaitForSeconds(3f);

        current_hole = false;

        if (nextHole != null)
        {
            nextHole.GetComponent<HoleData>().current_hole = true;
        }

        // so justTeleported = false	
        EventBus.Publish(new TeleportEvent());

        if (nextHole == null)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "WorldOne")
            {
                EventBus.Publish(new WorldUnlockedEvent(1));
            }
            else if(sceneName == "WorldTwo")
            {
                EventBus.Publish(new WorldUnlockedEvent(2));
            }
            EventBus.Publish(new LoadWorldEvent("HomeWorld"));
        } else
        {
            EventBus.Publish(new NewHoleEvent(nextHole.GetComponent<HoleData>()));
        }

        yield return null;
    }

    IEnumerator ResetHole()
    {
        EventBus.Publish<LevelFailedEvent>(new LevelFailedEvent());

        yield return new WaitForSeconds(3f);

        pins_down = 0;
        shots_taken = 0;
        EventBus.Publish<ResetPinsEvent>(new ResetPinsEvent());
        EventBus.Publish<ResetLivesEvent>(new ResetLivesEvent(numShots));
        GameObject.Find("ElectricBall").transform.position = initialBallPos;
        Camera.main.transform.position = initialCameraPos;
        Camera.main.transform.rotation = Quaternion.identity;
        Camera.main.transform.Rotate(initalCameraRotation);
        EventBus.Publish<ResetSplitEvent>(new ResetSplitEvent(this));
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

    public int GetHoleNumber()
    {
        return holeNumber;
    }

    private void ResetShot(ResetShotEvent e)
    {
        if (current_hole && e.position.x == -999)
        {
            EventBus.Publish(new ResetShotEvent(initialBallPos));
        }
    }
}
