using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    GameControl instance;
    float first_thrown_time;
    bool ball_thrown;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<LoadNextLevelEvent> level_subscription;

    private int level;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
        level_subscription = EventBus.Subscribe<LoadNextLevelEvent>(OnNewLevel);

        level = SceneManager.GetActiveScene().buildIndex;
    }

    void OnDestroy()
    {
        if (thrown_subscription != null) EventBus.Unsubscribe<BallThrownEvent>(thrown_subscription);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnBallThrown(BallThrownEvent e)
    {
        ball_thrown = true;
        first_thrown_time = Time.time;
    }

    void OnNewLevel(LoadNextLevelEvent e)
    {
        if (level + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(level + 1);
    }
}
