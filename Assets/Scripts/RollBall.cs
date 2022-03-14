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

    public float bar_time = 2.5f;

    private float bar_velocity;

    private bool windup = false;
    private bool rolling = false;

    private Rigidbody rb;
    private Transform tf;


    bool goingUp = true;
    float top;
    float bot;

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

        top = rt.rect.height;
        bot = -top;

        bar_velocity = (top - bot) / bar_time;

        ResetBar();
    }


    // Update is called once per frame
    void Update()
    {

        //move bar
        if (windup)
        {
            Vector3 barTf = bar.transform.localPosition;

            if (goingUp)
                barTf.y += bar_velocity;

            else
                barTf.y -= bar_velocity;

            bar.transform.localPosition = barTf;

            if (barTf.y >= top)
                goingUp = false;

            else if (barTf.y <= bot)
                goingUp = true;
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
        barTf.y = bot;
        goingUp = true;
        bar.transform.localPosition = barTf;
    }
}
