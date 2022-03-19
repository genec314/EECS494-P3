using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool current_hole = false;


    // Start is called before the first frame update
    void Awake()
    {
        pin_subscription = EventBus.Subscribe<PinKnockedOverEvent>(DecreasePins);
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(IncreaseShots);

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
        if(numPins == 0)
        {
            StartCoroutine(GoToNextHole());
        }
    }
    
    IEnumerator GoToNextHole() // Make a more generalizable public function that also calls this
    {
        current_hole = false;
        nextHole.GetComponent<HoleData>().current_hole = true;

        //We should make a toast system and send it "strike, spare etc depending on how many shots it took

        yield return new WaitForSeconds(3f);

        EventBus.Publish(new NewHoleEvent(nextHole.GetComponent<HoleData>()));

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

}
