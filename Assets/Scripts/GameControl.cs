using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    float first_thrown_time;
    bool ball_thrown;
    bool intro_played = false;
    Subscription<LoadNextLevelEvent> level_subscription;
    Subscription<ReloadLevelEvent> reload_subscription;
    Subscription<LoadIntroEvent> intro_subscription;

    private string levelName;
    int curr_world; // 0 = intro, otherwise 1, 2, 3
    int curr_level; // 0 to 9

    LevelData[] leveldata = new LevelData[10];
    LevelData[] world_2_levels = new LevelData[10];
    LevelData[] world_3_levels = new LevelData[10];

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        level_subscription = EventBus.Subscribe<LoadNextLevelEvent>(OnNewLevel);
        reload_subscription = EventBus.Subscribe<ReloadLevelEvent>(OnReloadLevel);

        levelName = SceneManager.GetActiveScene().name;
    }

    void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnIntroLevel(LoadIntroEvent e)
    {
        if (intro_played)
        {
            SceneManager.LoadScene("HomeWorld");
        }
        else
        {
            SceneManager.LoadScene("Intro");
        }
    }

    void OnNewLevel(LoadNextLevelEvent e)
    {
        string level_to_load = e.world;

        StartCoroutine(WaitThenLoadLevel(level_to_load));
    }

    void OnReloadLevel(ReloadLevelEvent e)
    {
        WaitThenLoadLevel(levelName);
    }

    IEnumerator WaitThenLoadLevel(string world)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(world);
        levelName = world;
        SceneManager.LoadScene(world);
    }

    public bool InTutorial()
    {
        return levelName == "HomeWorld";
    }
}

class LevelData {
    bool unlocked;
    bool complete;
    int pins;
    int max_throws;

    public LevelData()
    {
        unlocked = false;
        complete = false;
    }

    public void setUnlocked(bool _unlocked)
    {
        unlocked = _unlocked;
    }

    public void setCompleted(bool _complete)
    {
        complete = _complete;
    }
}
