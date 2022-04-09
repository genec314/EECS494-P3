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
        SceneManager.LoadScene("TitleScreen");
    }

    // Level select exit to home
    public void ExitToHome()
    {
        EventBus.Publish<LoadWorldEvent>(new LoadWorldEvent(0));
    }
}
