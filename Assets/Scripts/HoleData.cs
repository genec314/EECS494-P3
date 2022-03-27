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
    [SerializeField] int numberOfShots;
    [SerializeField] GameObject ball;
    int shots_taken = 0;
    [SerializeField] int holeNumber;
    [SerializeField] GameObject nextHole;
    Subscription<PinKnockedOverEvent> pin_subscription;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<BallReadyEvent> ball_ready_subscription;
    public bool current_hole = false;
    private bool inTransition = false;

    private int[] pointsOnShot = new int[3];


    // Start is called before the first frame update
    void Awake()
    {
        pin_subscription = EventBus.Subscribe<PinKnockedOverEvent>(DecreasePins);
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(IncreaseShots);
        ball_ready_subscription = EventBus.Subscribe<BallReadyEvent>(BallReady);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DecreasePins(PinKnockedOverEvent p)
    {
        if (current_hole == false)
        {
            return;
        }

        numPins--;
        // if(numPins == 0)
        // {
        //     if (!inTransition)
        //     {
        //         StartCoroutine(GoToNextHole());
        //     }
        // }
    }

    private void BallReady(BallReadyEvent e)
    {
        // takes into account that shots_taken was already incremented
        if (current_hole)
        {
            if (shots_taken == 1)
            {
                pointsOnShot[shots_taken - 1] = 10 - numPins;
            }
            else if (shots_taken == 2)
            {
                pointsOnShot[shots_taken - 1] = 10 - pointsOnShot[0] - numPins;
            }
            else if (shots_taken == 3)
            {
                pointsOnShot[shots_taken - 1] = 10 - pointsOnShot[0] - pointsOnShot[1] - numPins;
            }
        }

        if ((numberOfShots == shots_taken || numPins == 0) && current_hole)
        {
            if (!inTransition)
            {
                StartCoroutine(GoToNextHole());
            }
        }

    }
    
    IEnumerator GoToNextHole() // Make a more generalizable public function that also calls this
    {
        inTransition = true;

        if (current_hole)
        {
            if (shots_taken == 1)
            {
                pointsOnShot[shots_taken - 1] = 10 - numPins;
            }
            else if (shots_taken == 2)
            {
                pointsOnShot[shots_taken - 1] = 10 - pointsOnShot[0] - numPins;
            }
            else if (shots_taken == 3)
            {
                pointsOnShot[shots_taken - 1] = 10 - pointsOnShot[0] - pointsOnShot[1] - numPins;
            }
        }

        //We should make a toast system and send it "strike, spare etc depending on how many shots it took
        EventBus.Publish(new EndHoleEvent(this));

        yield return new WaitForSeconds(4f);

        current_hole = false;

        if (nextHole != null)
        {
            nextHole.GetComponent<HoleData>().current_hole = true;
        }

        if (nextHole == null)
        {
            EventBus.Publish(new LoadNextLevelEvent());
        } else
        {
            EventBus.Publish(new NewHoleEvent(nextHole.GetComponent<HoleData>()));
        }

        yield return null;
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
        return numberOfShots;
    }

    public int GetShotsTaken()
    {
        return shots_taken;
    }

    public int GetPinsRemaining()
    {
        return numPins;
    }

    public int GetHoleNumber()
    {
        return holeNumber;
    }

    public int[] GetScoresByShot()
    {
        return pointsOnShot;
    }
}
