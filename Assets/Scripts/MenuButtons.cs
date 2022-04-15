using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject settings_menu;
    public GameObject pause_menu;
    public GameObject world_menu;
    public GameObject level_select_menu;
    public GameObject menu_button;

    public bool in_level = false;
    public bool in_world = false;

    bool holeTransition = false;
    Subscription<LevelEndEvent> end_subscription;
    Subscription<LevelStartEvent> start_subscription;

    void Start()
    {
        end_subscription = EventBus.Subscribe<LevelEndEvent>(EndLevel);
        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
    }

    void Update()
    {
        if (in_level)
        {
            if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1)
            {
                PauseFromLevel();
            }
            else if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0)
            {
                ResumeFromLevel();
            }
            else if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 1 && !holeTransition)
            {
                RestartLevel();
            }
        }
        else if (in_world)
        {
            if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1)
            {
                PauseFromWorld();
            }
            else if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0)
            {
                if (world_menu != null && world_menu.activeSelf) ResumeFromWorld();
            }
        }
    }

    private void EndLevel(LevelEndEvent e)
    {
        holeTransition = true;
    }

    private void StartLevel(LevelStartEvent e)
    {
        holeTransition = false;
    }

    // Title screen menus
    public void PlayGame()
    {
        EventBus.Publish(new LoadIntroEvent());
    }

    public void SettingsToTitle()
    {
        if (settings_menu != null) settings_menu.SetActive(false);
    }

    public void SettingsFromTitle()
    {
        if (settings_menu != null) settings_menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Level pause menu
    public void PauseFromLevel()
    {
        Time.timeScale = 0;
        if (pause_menu != null) pause_menu.SetActive(true);
        if (menu_button != null) menu_button.SetActive(false);
    }

    public void ResumeFromLevel()
    {
        Time.timeScale = 1;
        if (pause_menu != null) pause_menu.SetActive(false);
        if (menu_button != null) menu_button.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        if (pause_menu != null) pause_menu.SetActive(false);
        if (menu_button != null) menu_button.SetActive(true);
        EventBus.Publish(new RestartLevelEvent());
    }

    public void LevelSelect()
    {
        Time.timeScale = 1;
        if (pause_menu != null) pause_menu.SetActive(false);
        if (menu_button != null) menu_button.SetActive(true);
        EventBus.Publish<LoadLevelSelectEvent>(new LoadLevelSelectEvent());
    }

    // World pause menu
    public void PauseFromWorld()
    {
        Time.timeScale = 0;
        if (world_menu != null) world_menu.SetActive(true);
        if (menu_button != null) menu_button.SetActive(false);
    }

    public void ResumeFromWorld()
    {
        Time.timeScale = 1;
        if (world_menu != null) world_menu.SetActive(false);
        if (menu_button != null) menu_button.SetActive(true);
    }

    public void SettingsToWorld()
    {
        if (world_menu != null) world_menu.SetActive(true);
        if (settings_menu != null) settings_menu.SetActive(false);
    }

    public void SettingsFromWorld()
    {
        if (world_menu != null) world_menu.SetActive(false);
        if (settings_menu != null) settings_menu.SetActive(true);
    }

    public void ExitToTitle()
    {
        Time.timeScale = 1;
        if (world_menu != null) world_menu.SetActive(false);
        if (menu_button != null) menu_button.SetActive(true);
        EventBus.Publish<LoadTitleEvent>(new LoadTitleEvent());
    }

    // Level select exit to home
    public void ExitToHome()
    {
        EventBus.Publish<LoadWorldEvent>(new LoadWorldEvent(0));
    }
}
