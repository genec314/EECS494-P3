using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePlayerController : MonoBehaviour
{
    Subscription<TutorialStrikeEvent> tutorial_strike_sub;

    GameObject camera;
    Rigidbody rb;

    bool in_select_mode = false;
    bool in_bowl_mode = true;
    bool doing_lerp = false;

    public GameObject[] selectables;
    public float lerp_time = 10.5f;

    int cur_selectable = 0;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("FirstPersonCam").gameObject;
        rb = GetComponent<Rigidbody>();
        tutorial_strike_sub = EventBus.Subscribe<TutorialStrikeEvent>(_OnTutorialStrike);
    }

    // Update is called once per frame
    void Update()
    {
        if (in_bowl_mode || doing_lerp)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {    
            if (!in_select_mode)
            {
                return;
            }
            string eventType = "Shop";
            switch (cur_selectable)
            {
                case 0:
                    eventType = "Shop";
                    break;
                case 1:
                    eventType = "Map";
                    break;
                /*
                case 2:
                    eventType = "HighScore";
                    break;
                */
                case 2:
                    eventType = "LeftLane";
                    break;
                case 3:
                    eventType = "MiddleLane";
                    break;
                case 4:
                    eventType = "RightLane";
                    break;
                default:
                    break;
            }
            in_select_mode = false;
            EventBus.Publish(new HomeWorldSelectEvent(eventType));

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            in_select_mode = true;
            SetSelectable(cur_selectable);
            EventBus.Publish(new HomeWorldExitEvent());
            EventBus.Publish<StopBarEvent>(new StopBarEvent());
        }
        else if (!in_select_mode)
        {
            return;
        }

        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {
            if(cur_selectable >= (selectables.Length - 1))
            {
                SetSelectable(0);           
            }
            else
            {
                SetSelectable(cur_selectable + 1);
            }
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {
            if(cur_selectable <= 0)
            {
                SetSelectable(selectables.Length - 1);
            }
            else
            {
                SetSelectable(cur_selectable - 1);
            }
        }
    }

    void _OnTutorialStrike(TutorialStrikeEvent e)
    {
        // transform.position = GameObject.Find("Main Camera").transform.position;
        // camera.transform.localRotation = GameObject.Find("Main Camera").transform.rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        in_select_mode = true;
        in_bowl_mode = false;
        SetSelectable(cur_selectable, 1f);
    }

    void SetSelectable(int index, float time = 0.5f)
    {
        cur_selectable = index;
        Vector3 oldPos = transform.position;
        Vector3 newPos = selectables[index].transform.position;
        
        //back wall
        if(index <= 1)
        {
            newPos.y = 0.5f;
            newPos.z += 10f;
            StartCoroutine(LerpPlayer(oldPos, newPos, transform.rotation.eulerAngles, new Vector3(0f, 180f, 0f), time));
            //transform.localPosition = new Vector3(0, -10f, -30f);
            //transform.localRotation = Quaternion.Euler(0f, 180f, 0);
        }
        /*
        else if(index == 2)
        {
            newPos.y = 0.5f;
            newPos.x += 10f;
            StartCoroutine(LerpPlayer(oldPos, newPos, transform.rotation.eulerAngles, new Vector3(0f, 90f, 0f)));
        }
        */
        //a lane
        else
        {
            newPos.y -= 7.75f;
            newPos.z -= 40f;
            StartCoroutine(LerpPlayer(oldPos, newPos, transform.rotation.eulerAngles, Vector3.zero, time));
            //transform.localPosition = new Vector3(0, -7f, -40f);
            //transform.localRotation = Quaternion.Euler(0f, 180f, 0);
        }
    }

    IEnumerator LerpPlayer(Vector3 startPos, Vector3 endPos, Vector3 oldRot, Vector3 newRot, float time)
    {
        doing_lerp = true;
        float elapsed = 0f;
        while(elapsed < time)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / lerp_time);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(oldRot, newRot, elapsed / lerp_time));
            elapsed += Time.deltaTime;
            yield return null;
        }

        doing_lerp = false;
    }
}
