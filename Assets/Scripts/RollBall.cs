using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollBall : MonoBehaviour
{
    public GameObject ball;
    public GameObject bar;

    public float power = 5f;

    private bool windup = false;
    private bool rolling = false;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!windup && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Image>().enabled = true;
            Image i = gameObject.GetComponentInChildren<Image>();
            Debug.Log(i);
            bar.SetActive(true);
            windup = true;
        }
        else if(windup && Input.GetKeyUp(KeyCode.Space))
        {
            windup = false;
            float meter = 10 - Mathf.Sqrt(Mathf.Abs(bar.transform.localPosition.y));
            rb.AddForce(new Vector3(0, 0, power*meter));

            GetComponent<Image>().enabled = false;
            bar.GetComponent<PowerBar>().Reset();
            bar.SetActive(false);

            EventBus.Publish(new BallThrownEvent(power * meter));
        }
    }
}
