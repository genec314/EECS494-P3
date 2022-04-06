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

    public int curr_world = 0; // 0 = intro, otherwise 1, 2, 3
    public int curr_level = 0; // 0 to 9

    bool world_1_visited = false;
    bool world_2_visited = false;
    bool world_3_visited = false;
    bool world_1_complete = false;
    bool world_2_complete = false;
    bool world_3_complete = false;
    public LevelData[] world_1_levels = new LevelData[10];
    public LevelData[] world_2_levels = new LevelData[10];
    public LevelData[] world_3_levels = new LevelData[10];

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
        Screen.SetResolution(1920, 1080, false);

        world_subscription = EventBus.Subscribe<LoadWorldEvent>(OnWorldChange);
        restart_subscription = EventBus.Subscribe<RestartLevelEvent>(OnRestartLevel);
        intro_subscription = EventBus.Subscribe<LoadIntroEvent>(OnIntroLevel);
        level_subscription = EventBus.Subscribe<LevelEndEvent>(OnLevelEnd);
        level_select_sub = EventBus.Subscribe<LoadLevelSelectEvent>(OnLoadLevelSelect);

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
            SceneManager.LoadScene("HomeWorld");
        }
        else
        {
            intro_played = true;
            // TODO: load intro/tutorial instead of homeworld when it exists
            SceneManager.LoadScene("HomeWorld");
        }
    }

    void OnRestartLevel(RestartLevelEvent e)
    {
        // if (curr_world == 1)
        // {
        //     SceneManager.LoadScene("WorldOne");
        // }
        // else if (curr_world == 2)
        // {
        //     SceneManager.LoadScene("WorldTwo");
        // }
        // else if (curr_world == 3)
        // {
        //     SceneManager.LoadScene("WorldThree");
        // }

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
        curr_world = e.world_num;
        if (e.world_num == 1)
        {
            if (world_1_visited)
            {
                SceneManager.LoadScene("WorldOneSelect");
            }
            else
            {
                curr_level = 0;
                world_1_levels[curr_level].setUnlocked(true);
                world_1_visited = true;
                SceneManager.LoadScene("WorldOne");
                LoadCurrentLevel();
            }
        }
        else if (e.world_num == 2)
        {
            if (world_2_visited)
            {
                SceneManager.LoadScene("WorldTwoSelect");
            }
            else
            {
                curr_level = 0;
                world_2_levels[curr_level].setUnlocked(true);
                world_2_visited = true;
                SceneManager.LoadScene("WorldTwo");
                LoadCurrentLevel();
            }
        }
        else if (e.world_num == 3)
        {
            if (world_3_visited)
            {
                SceneManager.LoadScene("WorldThreeSelect");
            }
            else
            {
                curr_level = 0;
                world_2_levels[curr_level].setUnlocked(true);
                world_3_visited = true;
                SceneManager.LoadScene("WorldThree");
                LoadCurrentLevel();
            }
        }
        else
        {
            SceneManager.LoadScene("HomeWorld");
        }
    }

    void OnLoadLevelSelect(LoadLevelSelectEvent e)
    {
        if (curr_world == 1)
        {
            SceneManager.LoadScene("WorldOneSelect");
        }
        else if (curr_world == 2)
        {
            SceneManager.LoadScene("WorldTwoSelect");
        }
        else if (curr_world == 3)
        {
            SceneManager.LoadScene("WorldThreeSelect");
        }
    }

    void OnSelectLevel(SelectLevelEvent e)
    {
        curr_level = e.level_num;
        LoadCurrentLevel();
    }

    IEnumerator HandleLevelComplete()
    {
        EventBus.Publish<LevelCompleteEvent>(new LevelCompleteEvent());

        yield return new WaitForSeconds(3f);

        if (curr_world == 1)
        {
            world_1_levels[curr_level].setCompleted(true);
        }
        else if (curr_world == 2)
        {
            world_2_levels[curr_level].setCompleted(true);
        }
        else if (curr_world == 3)
        {
            world_3_levels[curr_level].setCompleted(true);
        }

        // so justTeleported = false	
        EventBus.Publish(new TeleportEvent());

        if (curr_level < 9)
        {
            curr_level++;
            if (curr_world == 1)
            {
                world_1_levels[curr_level].setUnlocked(true);
            }
            else if (curr_world == 2)
            {
                world_2_levels[curr_level].setUnlocked(true);
            }
            else if (curr_world == 3)
            {
                world_3_levels[curr_level].setUnlocked(true);
            }
            LoadCurrentLevel();
        }
        else
        {
            if (curr_world == 1 && !world_1_complete)
            {
                world_1_complete = true;
                EventBus.Publish(new WorldUnlockedEvent(1));
                EventBus.Publish<WorldCompleteEvent>(new WorldCompleteEvent());
                yield return new WaitForSeconds(3f);
            }
            else if (curr_world == 2 && !world_2_complete)
            {
                world_2_complete = true;
                EventBus.Publish(new WorldUnlockedEvent(2));
                EventBus.Publish<WorldCompleteEvent>(new WorldCompleteEvent());
                yield return new WaitForSeconds(3f);
            }
            else if (curr_world == 3 && !world_3_complete)
            {
                // TODO: completion screen?
                world_3_complete = true;
                EventBus.Publish<WorldUnlockedEvent>(new WorldUnlockedEvent(3));
                EventBus.Publish<WorldCompleteEvent>(new WorldCompleteEvent());
                yield return new WaitForSeconds(3f);
            }

            curr_world = 0;
            EventBus.Publish(new LoadWorldEvent(0));
        }
    }

    IEnumerator HandleLevelFail()
    {
        EventBus.Publish<LevelFailedEvent>(new LevelFailedEvent());

        yield return new WaitForSeconds(3f);
        
        LoadCurrentLevel();
    }

    void LoadCurrentLevel()
    {
        EventBus.Publish<LoadLevelEvent>(new LoadLevelEvent(curr_level, curr_world));
    }

    public bool InTutorial()
    {
        return curr_world == 0;
    }

    void InitializeLevelData()
    {
        for (int i = 0; i < 10; i++)
        {
            world_1_levels[i] = new LevelData();
            world_2_levels[i] = new LevelData();
            world_3_levels[i] = new LevelData();
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
}
