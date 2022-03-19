using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleData : MonoBehaviour
{
    [SerializeField] Vector3 initialBallPos;
    [SerializeField] Vector3 initialCameraPos;
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

        //Wait some time to linger on the final shot

        //Lerp Camera to next hole's starting pos
        ball.transform.position = nextHole.GetComponent<HoleData>().initialBallPos;

        yield return null;
    }


    private void IncreaseShots(BallThrownEvent b)
    {
        if (current_hole)
        {
            shots_taken++;
            
        }
    }

}
