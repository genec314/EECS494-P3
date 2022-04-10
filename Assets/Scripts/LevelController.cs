using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int world;
    public int level;
    bool unlocked = false;
    bool completed = false;
    GameControl gc;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameControl").GetComponent<GameControl>();
        button = GetComponent<Button>();
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!unlocked && gc.level_data[world, level].isUnlocked()) {
            unlocked = true;
            button.interactable = true;
        }

        if (!completed && gc.level_data[world, level].isCompleted()) {
            completed = true;
        }
    }

    void ChangeToCompleted()
    {
        
    }

    public void LoadLevel()
    {
        EventBus.Publish<SelectLevelEvent>(new SelectLevelEvent(level));
    }
}
