using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChanger : MonoBehaviour
{

    public int currentLives = 3;
    UnityEngine.UI.Text text;

    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<NewHoleEvent> new_hole_subscription;
    Subscription<ResetLivesEvent> reset_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(DecreaseLives);
        new_hole_subscription = EventBus.Subscribe<NewHoleEvent>(NewHole);
        reset_subscription = EventBus.Subscribe<ResetLivesEvent>(ResetLevel);
        text = GetComponentInChildren<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DecreaseLives(BallThrownEvent b)
    {
        currentLives --;
        text.text = "x " + currentLives;
        if(currentLives < 1)
        {
            EventBus.Publish(new RanOutOfLivesEvent()); //I think we would need to set a bool in Gene's code for shots so that it moves to next hole after this shot
        }
    }

    private void NewHole(NewHoleEvent e)
    {
        currentLives = e.nextHole.GetNumShots();
        text.text = "x " + currentLives;
    }

    void ResetLevel(ResetLivesEvent e)
    {
        currentLives = e.lives;
        text.text = "x " + currentLives;
    }
}
