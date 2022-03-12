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
    }

    void OnDestroy()
    {
        if (thrown_subscription != null) EventBus.Unsubscribe<BallThrownEvent>(thrown_subscription);
    }

    // Update is called once per frame
    void Update()
    {
        if (ball_thrown && first_thrown_time + 5f < Time.time)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void OnBallThrown(BallThrownEvent e)
    {
        ball_thrown = true;
        first_thrown_time = Time.time;
    }
}
