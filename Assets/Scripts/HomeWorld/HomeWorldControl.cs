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
    Subscription<WorldUnlockedEvent> world_unlocked_sub;

    public GameObject tutorial_UI;
    public GameObject shop_UI;
    public GameObject map_UI;
    public GameObject high_score_UI;
    public GameObject throwball_UI;
    public GameObject controls_UI;

    GameObject curr_UI;

    private int pins_down = 0;
    private int attempts = 0;

    static HomeWorldControl instance;
    GameObject main_cam;
    public GameObject fpc;
    GameObject player;
    Vector3 playerStartPos;
    Vector3 lastThrowPos;

    private bool can_shoot = true;
    public bool can_free_move = true;
    private float sensitivity = 500f;

    [SerializeField]
    public GameObject[] toActivateLanes;

    HomeWorldData data;
    PlayerInventory pi;

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
        world_unlocked_sub = EventBus.Subscribe<WorldUnlockedEvent>(_OnWorldUnlocked);

        main_cam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        playerStartPos = player.transform.position;
        lastThrowPos = playerStartPos;
        tutorial_UI = GameObject.Find("TutorialUI");
        curr_UI = throwball_UI;

        data = GetComponent<HomeWorldData>();
        activeLanes = data.GetActiveLanes();
        Debug.Log(activeLanes);
        pi = GameObject.Find("GameControl").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnBallThrown(BallThrownEvent e)
    {
        if (this.enabled)
            tutorial_UI.SetActive(false);
    }

    void _OnPinKnocked(PinKnockedOverEvent e)
    {
        pins_down++;
        pi.AddPins(1);
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
            player.transform.position = lastThrowPos;

        }
        pins_down = 0;
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
            case "HighScore":
                high_score_UI.SetActive(true);
                curr_UI = high_score_UI;
                throwball_UI.SetActive(false);
                break;
            case "LeftLane":
                lastThrowPos = playerStartPos;
                lastThrowPos.x -= 15.1f;
                player.transform.position = lastThrowPos;
                SetCamera(lastThrowPos);
                cur_lane = 0;
                EnterLane();
                break;
            case "MiddleLane":
                player.transform.position = playerStartPos;
                lastThrowPos = playerStartPos;
                SetCamera(playerStartPos);
                cur_lane = 1;
                EnterLane();
                break;
            case "RightLane":
                lastThrowPos = playerStartPos;
                lastThrowPos.x += 15.1f;
                player.transform.position = lastThrowPos;
                SetCamera(lastThrowPos);
                cur_lane = 2;
                EnterLane();
                break;
        }
    }

    void EnterLane()
    {
        throwball_UI.SetActive(true);
        curr_UI = throwball_UI;
        controls_UI.SetActive(false);
        fpc.SetActive(false);
        main_cam.SetActive(true);
        player.transform.rotation = Quaternion.identity;
    }

    void SetCamera(Vector3 playerPos)
    {
        Vector3 camPos = playerPos;
        camPos.y = 6.5f;
        camPos.z = -19f;
        main_cam.transform.position = camPos;
    }

    void _OnExit(HomeWorldExitEvent e)
    {
        curr_UI.SetActive(false);
        controls_UI.SetActive(true);
        fpc.SetActive(true);
        main_cam.SetActive(false);
    }

    void _OnWorldUnlocked(WorldUnlockedEvent e)
    {
        toActivateLanes[e.num].SetActive(true);
        activeLanes[e.num] = true;
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
        controls_UI.SetActive(true);
        EventBus.Publish(new ResetPinsEvent());
    }

    IEnumerator NextLevel(float time)
    {
        yield return new WaitForSeconds(time);
        EventBus.Publish(new LoadNextLevelEvent());
        this.enabled = false;
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        this.enabled = true;
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
