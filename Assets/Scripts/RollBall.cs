using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollBall : MonoBehaviour
{
    public GameObject bar;
    public GameObject meter;

    public float power = 5f;
    private float progress = 0;

    public float time = 2f;
    private float initial_time = 0;

    private float bar_velocity;

    private bool windup = false;
    private bool rolling = false;
    private bool startMove = false;

    bool goingUp = true;
    Vector3 top;
    Vector3 bot;

    RollBall instance;
    // Start is called before the first frame update
    void Start()
    {
        meter.SetActive(false);
        bar.SetActive(false);

        RectTransform rt = meter.GetComponentInChildren<RectTransform>();

        top = new Vector3(rt.localPosition.x, rt.rect.height - 4, 0);
        bot = new Vector3(rt.localPosition.x, -rt.rect.height + 4, 0);

        ResetBar();
    }


    // Update is called once per frame
    void Update()
    {
        MoveBar();
        ControlBar();
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
            windup = false;

            float force = ((bar.transform.localPosition.y - bot.y)/(top.y - bot.y))*10;
            EventBus.Publish(new BallThrownEvent(power * force));
            
            meter.SetActive(false);
            bar.SetActive(false);
            ResetBar();
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

            if (progress > 1)
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
}
