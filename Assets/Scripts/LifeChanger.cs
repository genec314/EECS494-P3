using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeChanger : MonoBehaviour
{

    public int currentLives = 3;
    TextMeshProUGUI text;

    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<LevelStartEvent> start_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(DecreaseLives);
        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void DecreaseLives(BallThrownEvent b)
    {
        currentLives--;
        UpdateText();
    }

    void StartLevel(LevelStartEvent e)
    {
        currentLives = e.level.GetNumShots();
        UpdateText();
    }

    void UpdateText()
    {
        text.text = "x " + currentLives;
    }
}
