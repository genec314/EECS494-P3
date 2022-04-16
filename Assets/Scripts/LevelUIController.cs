using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUIController : MonoBehaviour
{
    TextMeshProUGUI text;
    Subscription<LevelStartEvent> start_sub;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        start_sub = EventBus.Subscribe<LevelStartEvent>(OnLevelStart);
    }

    void OnLevelStart(LevelStartEvent e)
    {
        if (e.level.worldNumber == 3)
        {
            text.text = "Level 1/1";
        }
        else
        {
            text.text = "Level " + (e.level.levelNumber + 1) + "/7";
        }
    }
}
