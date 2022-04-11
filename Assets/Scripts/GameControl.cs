using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    bool intro_played = false;
    Subscription<LoadWorldEvent> world_subscription;
    Subscription<RestartLevelEvent> restart_subscription;
    Subscription<LoadIntroEvent> intro_subscription;
    Subscription<LevelEndEvent> level_subscription;
    Subscription<LoadLevelSelectEvent> level_select_sub;
    Subscription<SelectLevelEvent> select_level_sub;
    Subscription<LoadTitleEvent> title_sub;

    public int curr_world = 0; // 0 = intro, otherwise 1, 2, 3
    public int curr_level = 0; // 0 to 9

    public int pin_reward = 10;

    bool world_1_visited = false;
    bool world_2_visited = false;
    bool world_3_visited = false;
    bool world_1_complete = false;
    bool world_2_complete = false;
    bool world_3_complete = false;
    public LevelData[,] level_data = new LevelData[3, 7];

    public bool tutorial_initial = false;

    // these three are for world 2
    public bool tutorial_hole4 = false;
    public bool tutorial_firstF = true;
    public bool tutorial_ended = false;

    public float transition_duration = 1f;

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
        Screen.SetResolution(1920, 1080, true);

        world_subscription = EventBus.Subscribe<LoadWorldEvent>(OnWorldChange);
        restart_subscription = EventBus.Subscribe<RestartLevelEvent>(OnRestartLevel);
        intro_subscription = EventBus.Subscribe<LoadIntroEvent>(OnIntroLevel);
        level_subscription = EventBus.Subscribe<LevelEndEvent>(OnLevelEnd);
        level_select_sub = EventBus.Subscribe<LoadLevelSelectEvent>(OnLoadLevelSelect);
        select_level_sub = EventBus.Subscribe<SelectLevelEvent>(OnSelectLevel);
        title_sub = EventBus.Subscribe<LoadTitleEvent>(OnLoadTitle);

        InitializeLevelData();
    }

    void OnDestroy()
    {
        
    }

    void OnIntroLevel(LoadIntroEvent e)
    {
        curr_world = 0;
        if (intro_played)
        {
            StartCoroutine(LoadWorldAndLevel(curr_world));
        }
        else
        {
            intro_played = true;
            // TODO: load intro/tutorial instead of homeworld when it exists
            // SceneManager.LoadScene("Intro");
            StartCoroutine(LoadWorldAndLevel(curr_world));
        }
    }

    void OnLoadTitle(LoadTitleEvent e)
    {
        StartCoroutine(LoadTitle());
    }

    void OnRestartLevel(RestartLevelEvent e)
    {
        EventBus.Publish<LoadLevelEvent>(new LoadLevelEvent(curr_level, curr_world));
    }

    void OnLevelEnd(LevelEndEvent e)
    {
        if (e.complete)
        {
            StartCoroutine(HandleLevelComplete());
        }
        else
        {
            StartCoroutine(HandleLevelFail());
        }
    }

    // For when the player switches worlds. Should handle updating world_num and loading the appropriate level select
    void OnWorldChange(LoadWorldEvent e)
    {
        curr_world = e.world_num - 1;
        if (e.world_num == 1)
        {
            if (world_1_visited)
            {
                EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
            }
            else
            {
                curr_level = 0;
                level_data[curr_world, curr_level].setUnlocked(true);
                world_1_visited = true;
                StartCoroutine(LoadWorldAndLevel(curr_world));
            }
        }
        else if (e.world_num == 2)
        {
            if (world_2_visited)
            {
                EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
            }
            else
            {
                curr_level = 0;
                level_data[curr_world, curr_level].setUnlocked(true);
                world_2_visited = true;
                StartCoroutine(LoadWorldAndLevel(curr_world));
            }
        }
        else if (e.world_num == 3)
        {
            // if (world_3_visited)
            // {
            //     EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
            // }
            // else
            // {
                curr_level = 0;
                level_data[curr_world, curr_level].setUnlocked(true);
                // world_3_visited = true;
                StartCoroutine(LoadWorldAndLevel(curr_world));
            // }
        }
        else
        {
            StartCoroutine(LoadWorldAndLevel(0));
        }
    }

    void OnLoadLevelSelect(LoadLevelSelectEvent e)
    {
        StartCoroutine(LoadLevelSelect(curr_world));
    }

    void OnSelectLevel(SelectLevelEvent e)
    {
        curr_level = e.level_num;
        StartCoroutine(LoadWorldAndLevel(curr_world));
    }

    IEnumerator HandleLevelComplete()
    {
        EventBus.Publish<LevelCompleteEvent>(new LevelCompleteEvent());

        if (!level_data[curr_world, curr_level].isCompleted())
        {
            level_data[curr_world, curr_level].setCompleted(true);
            EventBus.Publish<GainPinsEvent>(new GainPinsEvent(pin_reward));
        }
        else
        {
            EventBus.Publish<GainPinsEvent>(new GainPinsEvent(pin_reward/2));
        }

        yield return new WaitForSeconds(2.5f);

        // so justTeleported = false	
        EventBus.Publish(new TeleportEvent());

        if (curr_level < 6 && curr_world != 3)
        {
            // put check for end of world 3 here cause only three levels

            curr_level++;
            level_data[curr_world, curr_level].setUnlocked(true);
            LoadCurrentLevel();
        }
        else
        {
            if (curr_world == 1)
            {
                if (!world_1_complete)
                {
                    world_1_complete = true;
                    EventBus.Publish(new WorldUnlockedEvent(1));
                    EventBus.Publish<WorldCompleteEvent>(new WorldCompleteEvent());
                    yield return new WaitForSeconds(3f);
                    curr_world = 0;
                    EventBus.Publish(new LoadWorldEvent(0));
                }
                else
                {
                    EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
                }
            }
            else if (curr_world == 2)
            {
                if (!world_2_complete)
                {
                    world_2_complete = true;
                    EventBus.Publish(new WorldUnlockedEvent(2));
                    EventBus.Publish<WorldCompleteEvent>(new WorldCompleteEvent());
                    yield return new WaitForSeconds(3f);
                    curr_world = 0;
                    EventBus.Publish(new LoadWorldEvent(0));
                }
                else
                {
                    EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
                }
            }
            else if (curr_world == 3)
            {
                if (!world_3_complete)
                {
                    // TODO: unlock some sort of "exit" in the home world that leads to completion
                    world_3_complete = true;
                    EventBus.Publish<WorldUnlockedEvent>(new WorldUnlockedEvent(3));
                    EventBus.Publish<WorldCompleteEvent>(new WorldCompleteEvent());
                    yield return new WaitForSeconds(3f);
                    curr_world = 0;
                    EventBus.Publish(new LoadWorldEvent(0));
                }
                else
                {
                    EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
                }
            }
        }
    }

    IEnumerator HandleLevelFail()
    {
        EventBus.Publish<LevelFailedEvent>(new LevelFailedEvent());

        yield return new WaitForSeconds(2f);
        
        LoadCurrentLevel();
    }

    void LoadCurrentLevel()
    {
        EventBus.Publish<LoadLevelEvent>(new LoadLevelEvent(curr_level, curr_world));
    }

    public bool InHomeWorld()
    {
        return curr_world == 0;
    }

    IEnumerator LoadWorldAndLevel(int world)
    {
        EventBus.Publish<SceneTransitionEvent>(new SceneTransitionEvent());

        yield return new WaitForSeconds(transition_duration);

        AsyncOperation load = null;
        if (world == 1)
        {
            load = SceneManager.LoadSceneAsync("WorldOne");
        }
        else if (world == 2)
        {
            load = SceneManager.LoadSceneAsync("WorldTwo");
        }
        else if (world == 3)
        {
            load = SceneManager.LoadSceneAsync("WorldThree");
        }
        else
        {
            load = SceneManager.LoadSceneAsync("HomeWorld");
        }

        while (!load.isDone)
        {
            yield return null;
        }

        if (world != 0) LoadCurrentLevel();
    }

    IEnumerator LoadLevelSelect(int world)
    {
        EventBus.Publish<SceneTransitionEvent>(new SceneTransitionEvent());

        yield return new WaitForSeconds(transition_duration);

        if (world == 1)
        {
            SceneManager.LoadScene("WorldOneSelect");
        }
        else if (world == 2)
        {
            SceneManager.LoadScene("WorldTwoSelect");
        }
        else if (world == 3)
        {
            SceneManager.LoadScene("WorldThreeSelect");
        }
    }

    IEnumerator LoadTitle()
    {
        EventBus.Publish<SceneTransitionEvent>(new SceneTransitionEvent());

        yield return new WaitForSeconds(transition_duration);

        SceneManager.LoadScene("TitleScreen");
    }

    void InitializeLevelData()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                level_data[i, j] = new LevelData();
            }
        }
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

    public bool isUnlocked()
    {
        return unlocked;
    }

    public bool isCompleted()
    {
        return complete;
    }
}
