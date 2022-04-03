using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeWorldControl : MonoBehaviour
{

    Subscription<BallThrownEvent> ball_thrown_sub;
    Subscription<PinKnockedOverEvent> pin_knocked_sub;
    Subscription<BallReadyEvent> ball_ready_subscription;
    Subscription<HomeWorldSelectEvent> select_sub;
    Subscription<HomeWorldExitEvent> exit_sub;

    public GameObject tutorial_UI;
    public GameObject shop_UI;
    public GameObject map_UI;
    //public GameObject high_score_UI;
    public GameObject throwball_UI;
    GameObject curr_UI;

    private int pins_down = 0;
    private int attempts = 0;

    static HomeWorldControl instance;
    GameObject main_cam;
    public GameObject fpc;
    GameObject player;
    Vector3 playerStartPos;

    private bool can_shoot = true;
    public bool can_free_move = true;
    private float sensitivity = 500f;

    [SerializeField]
    public GameObject[] pinReset;

    HomeWorldData data;
    //true if the world that lane is unlocked
    bool[] activeLanes;
    private int cur_lane = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;

        ball_thrown_sub = EventBus.Subscribe<BallThrownEvent>(_OnBallThrown);
        pin_knocked_sub = EventBus.Subscribe<PinKnockedOverEvent>(_OnPinKnocked);
        ball_ready_subscription = EventBus.Subscribe<BallReadyEvent>(_OnBallReady);
        select_sub = EventBus.Subscribe<HomeWorldSelectEvent>(_OnSelect);
        exit_sub = EventBus.Subscribe<HomeWorldExitEvent>(_OnExit);

        main_cam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        playerStartPos = player.transform.position;
        tutorial_UI = GameObject.Find("TutorialUI");

        data = GetComponent<HomeWorldData>();
        activeLanes = data.GetActiveLanes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnBallThrown(BallThrownEvent e)
    {
        tutorial_UI.SetActive(false);
    }

    void _OnPinKnocked(PinKnockedOverEvent e)
    {
        pins_down++;
        if(pins_down >= 10)
        {
            if (activeLanes[cur_lane])
            {
                StartCoroutine(NextLevel(3f));
            }
            else
            {
                StartCoroutine(ExitTutorial());
            }
            
        }
    }

    void _OnBallReady(BallReadyEvent e)
    {
        if (pins_down < 10)
        {
            EventBus.Publish(new ResetPinsEvent());
            pins_down = 0;
            player.transform.position = playerStartPos;

        }
    }

    void _OnSelect(HomeWorldSelectEvent e)
    {
        switch (e.where)
        {
            case "Shop":
                shop_UI.SetActive(true);
                curr_UI = shop_UI;
                throwball_UI.SetActive(false);
                break;
            case "Map":
                map_UI.SetActive(true);
                curr_UI = map_UI;
                throwball_UI.SetActive(false);
                break;
        }
    }

    void _OnExit(HomeWorldExitEvent e)
    {
        curr_UI.SetActive(false);
    }
    IEnumerator ExitTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        EventBus.Publish(new TutorialStrikeEvent());
        can_free_move = true;
        can_shoot = false;
        main_cam.SetActive(false);
        fpc.SetActive(true);
        throwball_UI.SetActive(false);
        EventBus.Publish(new ResetPinsEvent());
    }

    IEnumerator NextLevel(float time)
    {
        yield return new WaitForSeconds(time);
        EventBus.Publish(new LoadNextLevelEvent());
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        tutorial_UI.GetComponentInChildren<TextMeshProUGUI>().text = "Welcome to my bowling alley! Hold Space to Shoot!";
    }

    public bool CanShoot()
    {
        return can_shoot;
    }

    public void SetCanShoot(bool _in)
    {
        can_shoot = _in;
    }

    public bool CanFreeMove()
    {
        return can_free_move;
    }

    public void SetCanFreeMove(bool _in)
    {
        can_free_move = _in;
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }

    public void SetSensitivity(float _in)
    {
        sensitivity = _in;
    }


}
