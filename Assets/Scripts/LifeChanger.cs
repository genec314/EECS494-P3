using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeChanger : MonoBehaviour
{

    public int currentLives = 3;
    Text text;

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

    private void DecreaseLives(BallThrownEvent b)
    {
        currentLives --;
        UpdateText();
    }

    private void NewHole(NewHoleEvent e)
    {
        currentLives = e.nextHole.GetNumShots();
        UpdateText();
    }

    void ResetLevel(ResetLivesEvent e)
    {
        currentLives = e.lives;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = "x " + currentLives;
    }
}
