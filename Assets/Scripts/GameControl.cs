using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    float first_thrown_time;
    bool ball_thrown;
    Subscription<BallThrownEvent> thrown_subscription;
    Subscription<LoadNextLevelEvent> level_subscription;
    Subscription<ReloadLevelEvent> reload_subscription;

    private int level;


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

        thrown_subscription = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
        level_subscription = EventBus.Subscribe<LoadNextLevelEvent>(OnNewLevel);
        reload_subscription = EventBus.Subscribe<ReloadLevelEvent>(OnReloadLevel);


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
        return level == 0;
    }
}

