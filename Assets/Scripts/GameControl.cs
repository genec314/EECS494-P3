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

    LevelData[][] leveldata = new LevelData[3][10];
    LevelData[] world_2_levels = new LevelData[10];
    LevelData[] world_3_levels = new LevelData[10];

    int level;

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

        level = SceneManager.GetActiveScene().buildIndex;
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
            SceneManager.LoadScene("home");
        }
        else
        {
            SceneManager.LoadScene("intro");
        }
    }

    void OnNewLevel(LoadNextLevelEvent e)
    {
        if (level + 1 == SceneManager.sceneCountInBuildSettings)
        {
            level = 0;
            StartCoroutine(WaitThenLoadLevel(0));
            return;
        }

        StartCoroutine(WaitThenLoadLevel(level++ + 1));
    }

    void OnReloadLevel(ReloadLevelEvent e)
    {
        WaitThenLoadLevel(level);
    }

    IEnumerator WaitThenLoadLevel(int level_num)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level_num);
    }

    public bool InTutorial()
    {
        return levelName == "Intro";
    }
}

class LevelData {
    bool unlocked;
    int pins;
    int pins_down;
    int max_throws;

    public LevelData()
    {
        unlocked = false;
        pins = 10;
        pins_down = 0;
        max_throws = 3;
    }

    public LevelData(int _pins, int _pins_down, int _max_throws)
    {
        pins = _pins;
        pins_down = _pins_down;
        max_throws = _max_throws;
    }
}
