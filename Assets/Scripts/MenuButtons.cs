using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject settings_menu;
    public GameObject pause_menu;
    public GameObject world_menu;

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
    }

    public void ResumeFromLevel()
    {
        Time.timeScale = 1;
        if (pause_menu != null) pause_menu.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        if (pause_menu != null) pause_menu.SetActive(false);
        EventBus.Publish(new ReloadLevelEvent());
    }

    public void LevelSelect()
    {
        Time.timeScale = 1;
        if (pause_menu != null) pause_menu.SetActive(false);
    }

    // World pause menu
    public void PauseFromWorld()
    {
        Time.timeScale = 0;
        if (world_menu != null) world_menu.SetActive(true);
    }

    public void ResumeFromWorld()
    {
        Time.timeScale = 1;
        if (world_menu != null) world_menu.SetActive(false);
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
        if (world_menu != null) world_menu.SetActive(false);
        SceneManager.LoadScene("title");
    }
}
