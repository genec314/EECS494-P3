using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Should be renamed to something like "Power Bar Controller"
// This only handles the power bar UI and sending events. It does not directly control the movement of the ball; that is in Move.
public class RollBall : MonoBehaviour
{
    public GameObject bar;
    public GameObject meter;
    public static bool canMove = true;
    private float last_thrown;

    public float power = 5f;
    private float progress = 0;

    public float time = 2f;
    private float initial_time = 0;

    private float bar_velocity;

    private bool windup = false;
    private bool startMove = false;
    bool holeTransition = false;

    bool goingUp = true;
    Vector3 top;
    Vector3 bot;

    RollBall instance;

    Subscription<BallReadyEvent> ready_subscription;
    Subscription<LevelEndEvent> end_subscription;
    Subscription<LevelStartEvent> start_subscription;
    Subscription<StopBarEvent> stop_sub;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        meter.SetActive(false);
        bar.SetActive(false);

        RectTransform rt = meter.GetComponentInChildren<RectTransform>();

        top = new Vector3(rt.localPosition.x, rt.rect.height - 10, 0);
        bot = new Vector3(rt.localPosition.x, -rt.rect.height + 5, 0);

        ResetBar();

        ready_subscription = EventBus.Subscribe<BallReadyEvent>(CheckForMove);
        end_subscription = EventBus.Subscribe<LevelEndEvent>(EndLevel);
        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
        stop_sub = EventBus.Subscribe<StopBarEvent>(StopBar);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallReadyEvent>(ready_subscription);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBar();
        if (canMove && !holeTransition && Time.timeScale != 0) ControlBar();
    }

    void CheckForMove(BallReadyEvent e)
    {
        canMove = true;
    }

    private void EndLevel(LevelEndEvent e)
    {
        holeTransition = true;
        CancelBar();
    }

    private void StartLevel(LevelStartEvent e)
    {
        holeTransition = false;
        canMove = true;
        CancelBar();
    }

    void StopBar(StopBarEvent e)
    {
        CancelBar();
    }

    private void ControlBar()
    {
        //show meter
        if (!windup && Input.GetKeyDown(KeyCode.Space))
        {
            meter.SetActive(true);
            bar.SetActive(true);
            windup = true;
        }
        //throw ball
        else if (windup && Input.GetKeyUp(KeyCode.Space))
        {
            float force = ((bar.transform.localPosition.y - bot.y)/(top.y - bot.y))*10 + 1;
            EventBus.Publish(new BallThrownEvent(power * force));
            canMove = false;
            CancelBar();
        }
    }

    private void MoveBar()
    {
        //move bar
        if (windup)
        {
            if (!startMove)
            {
                startMove = true;
                initial_time = Time.time;
            }

            Vector3 barTf = bar.transform.localPosition;

            progress = (Time.time - initial_time) / time;

            if (goingUp)
                barTf = Vector3.Lerp(bot, top, progress);

            else
                barTf = Vector3.Lerp(top, bot, progress);

            bar.transform.localPosition = barTf;

            if (progress >= 1)
            {
                startMove = false;
                goingUp = !goingUp;
            }
        }
    }

    private void ResetBar()
    {
        Vector3 barTf = bar.transform.localPosition;
        barTf = bot;
        goingUp = true;
        bar.transform.localPosition = barTf;
        startMove = false;
    }

    void CancelBar()
    {
        meter.SetActive(false);
        bar.SetActive(false);
        windup = false;
        ResetBar();
    }
}
