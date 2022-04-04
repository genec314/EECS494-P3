using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    bool intro_played = false;
    Subscription<LoadNextLevelEvent> level_subscription;
    Subscription<ReloadLevelEvent> reload_subscription;
    Subscription<LoadIntroEvent> intro_subscription;

    int curr_world = 0; // 0 = intro, otherwise 1, 2, 3
    int curr_level = 0; // 0 to 9

    public LevelData[] world_1_levels = new LevelData[10];
    public LevelData[] world_2_levels = new LevelData[10];
    public LevelData[] world_3_levels = new LevelData[10];

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
        intro_subscription = EventBus.Subscribe<LoadIntroEvent>(OnIntroLevel);

        level = SceneManager.GetActiveScene().buildIndex;
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
        curr_world = 0;
        if (intro_played)
        {
            SceneManager.LoadScene("HomeWorld");
        }
        else
        {
            // SceneManager.LoadScene("Intro");
            SceneManager.LoadScene("HomeWorld");
        }
    }

    void OnLevelUpdate(UpdateLevelDataEvent e)
    {
        if (e.world_num == 1)
        {
            world_1_levels[e.level_num].setCompleted(e.complete);
        }
        else if (e.world_num == 2)
        {
            world_2_levels[e.level_num].setCompleted(e.complete);
        }
        else if (e.world_num == 3)
        {
            world_3_levels[e.level_num].setCompleted(e.complete);
        }

        // TODO: determine the next level to unlock/load, then load it. If end of world is reached, go back to home world
    }

    // TODO: for when the player switches worlds. should handle updating world_num and loading the appropriate level select
    void OnWorldChange()
    {

    }

    // TODO: load the appropriate level select screen
    void OnLoadLevelSelect()
    {

    }

    // will be deprecated in favor of having game control dictate loading new levels
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
        EventBus.Publish<LoadLevelEvent>(new LoadLevelEvent(curr_level, curr_world));
    }

    public bool InTutorial()
    {
        return curr_world == 0;
    }
}

public class LevelData {
    bool unlocked;
    bool complete;

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
