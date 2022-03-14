using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollBall : MonoBehaviour
{
    public GameObject ball;
    public GameObject bar;
    public GameObject meter;

    public float power = 5f;

    public int bar_frames = 120;

    private float bar_velocity;

    private bool windup = false;
    private bool rolling = false;

    private Rigidbody rb;
    private Transform tf;


    bool goingUp = true;
    Vector3 top;
    Vector3 bot;
    int elapsedFrames = 0;

    RollBall instance;
    // Start is called before the first frame update
    void Start()
    {


        meter.SetActive(false);
        bar.SetActive(false);

        if (!ball)
        {
            ball = GameObject.Find("Ball");
        }
        rb = ball.GetComponent<Rigidbody>();
        tf = ball.GetComponent<Transform>();

        RectTransform rt = meter.GetComponentInChildren<RectTransform>();

        top = new Vector3(200, rt.rect.height, 0);
        bot = new Vector3(200, -rt.rect.height, 0);

        ResetBar();
    }


    // Update is called once per frame
    void Update()
    {
        elapsedFrames++;
        //move bar
        if (windup)
        {
            Vector3 barTf = bar.transform.localPosition;

            if (goingUp)
                barTf = Vector3.Lerp(bot, top, (float)(elapsedFrames) / bar_frames);

            else
                barTf = Vector3.Lerp(top, bot, (float)(elapsedFrames) / bar_frames);

            bar.transform.localPosition = barTf;

            if(elapsedFrames >= bar_frames)
            {
                goingUp = !goingUp;
                elapsedFrames = 0;
            }
        }

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

            float force = 10 - Mathf.Sqrt(Mathf.Abs(bar.transform.localPosition.y));
            rb.AddForce(tf.transform.forward * power * force);

            ResetBar();
            meter.SetActive(false);
            bar.SetActive(false);

            EventBus.Publish(new BallThrownEvent(power * force));
        }
    }

    private void ResetBar()
    {
        Vector3 barTf = bar.transform.localPosition;
        barTf = bot;
        goingUp = true;
        bar.transform.localPosition = barTf;
        elapsedFrames = 0;
    }
}
