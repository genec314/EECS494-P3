using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeWorldControl : MonoBehaviour
{
    // The two places the toast UI panel alternates between.
    public Vector3 hidden_pos = new Vector3(0f, -200f, 0f);
    public Vector3 visible_pos = new Vector3(0f, 0f, 0f);

    // These inspector-accessible variables control how the toast UI panel moves between the hidden and visible positions.
    public AnimationCurve ease;
    public AnimationCurve ease_out;

    // Duration controls.
    public float ease_duration = 0.5f;

    Subscription<BallThrownEvent> ball_thrown_sub;
    Subscription<PinKnockedOverEvent> pin_knocked_sub;
    Subscription<BallReadyEvent> ball_ready_subscription;
    Subscription<HomeWorldSelectEvent> select_sub;
    Subscription<HomeWorldExitEvent> exit_sub;
    Subscription<WorldUnlockedEvent> world_unlocked_sub;

    public RectTransform tutorial_UI;
    public GameObject shop_UI;
    public GameObject map_UI;
    //public GameObject high_score_UI;
    public GameObject throwball_UI;
    public GameObject controls_UI;
    public GameObject transition_UI;

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

    [SerializeField]
    public GameObject[] toActivateLanes;
    public AudioClip activate;
    private int[] intensities;

    HomeWorldData data;
    PlayerInventory pi;

    List<int> unlocked_worlds;
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
        curr_UI = throwball_UI;

        data = GameObject.Find("GameControl").GetComponent<HomeWorldData>();
        unlocked_worlds = data.GetUnlockedWorlds();

        for(int i = 0; i < unlocked_worlds.Count - 1; i++)
        {
            toActivateLanes[unlocked_worlds[i]].SetActive(true);
        }
        intensities = new int[] { 5000, 6, 20 };

        if(unlocked_worlds.Count == 2 || unlocked_worlds.Count == 3)
        {
<<<<<<< HEAD
            StartCoroutine(TurnOnLights(unlocked_worlds.Count - 1));
        }

=======
            can_free_move = true;
            can_shoot = false;
            main_cam.SetActive(false);
            fpc.SetActive(true);
            throwball_UI.SetActive(false);
            controls_UI.SetActive(true);
            EventBus.Publish(new TutorialStrikeEvent());
        }
        else
        {
            StartCoroutine(EaseIn(tutorial_UI));
        }
>>>>>>> 26d0d511e5b79e954459842ed519e008ee914909
        pi = GameObject.Find("GameControl").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnBallThrown(BallThrownEvent e)
    {
        if (this.enabled && unlocked_worlds.Count < 1)
            StartCoroutine(EaseOut(tutorial_UI));
    }

    void _OnPinKnocked(PinKnockedOverEvent e)
    {
        pins_down++;
        if(pins_down >= 10)
        {
            if (unlocked_worlds.Contains(cur_lane))
            {
                switch (cur_lane)
                {
                    case 0:
                        StartCoroutine(NextLevel(1f, 1));
                        break;
                    case 1:
                        StartCoroutine(NextLevel(1f, 2));
                        break;
                    case 2:
                        StartCoroutine(NextLevel(1f, 3));
                        break;
                }
                
            }
            else if(unlocked_worlds.Count <= 0)
            {
                StartCoroutine(ExitLane(true));
            }
            else
            {
                StartCoroutine(ExitLane(false));
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
            /*
            case "HighScore":
                high_score_UI.SetActive(true);
                curr_UI = high_score_UI;
                throwball_UI.SetActive(false);
                break;
            */
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
        //toActivateLanes[e.num].SetActive(true);
        //unlocked_worlds.Add(e.num);
        StartCoroutine(TurnOnLights(e.num));
    }

    IEnumerator TurnOnLights(int num)
    {
        Vector3 orig_position = main_cam.transform.localPosition;
        Vector3 orig_rotation = main_cam.transform.localRotation.eulerAngles;

        Vector3 newPos = new Vector3(0, 1, 0);
        newPos.x += (num - 1) * 15.13f;
        Vector3 newRot = new Vector3(-10, 0, 0);
        float cam_duration = 0.6f;
        float elapsed = 0;
        GetComponent<AudioSource>().PlayOneShot(activate);
        while(elapsed < cam_duration)
        {
            main_cam.transform.localPosition = Vector3.Lerp(orig_position, newPos, elapsed / cam_duration);
            main_cam.transform.localRotation = Quaternion.Euler(Vector3.Lerp(orig_rotation, newRot, elapsed / cam_duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        float duration = 3f;
        elapsed = 0;

        Light[] lights = toActivateLanes[num].GetComponentsInChildren<Light>();
        toActivateLanes[num].SetActive(true);
        while(elapsed < duration)
        {
            for(int i = 0; i < lights.Length; i++)
            {
                lights[i].intensity = intensities[i] * (elapsed / duration);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        main_cam.transform.localPosition = orig_position;
        main_cam.transform.localRotation = Quaternion.Euler(orig_rotation);
        AfterStrike();
    }

    IEnumerator ExitLane(bool tutorial)
    {
        yield return new WaitForSeconds(1f);
        if (tutorial)
        {
            EventBus.Publish(new WorldUnlockedEvent(0));
        }
        else
        {
            AfterStrike();
        }
    }

    void AfterStrike()
    {
        EventBus.Publish(new TutorialStrikeEvent());
        can_free_move = true;
        can_shoot = false;
        main_cam.SetActive(false);
        fpc.SetActive(true);
        throwball_UI.SetActive(false);
        controls_UI.SetActive(true);
        EventBus.Publish(new ResetPinsEvent());
    }

    IEnumerator NextLevel(float time, int level)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("Menu").SetActive(false);

        float up_duration = 1.5f;
        float elapsed = 0;
        Vector3 orig_pos = main_cam.transform.position;
        Vector3 up_pos = main_cam.transform.position;
        Vector3 orig_rot = main_cam.transform.rotation.eulerAngles;
        up_pos.z = -25f;
        up_pos.y = 6;
        while(elapsed < up_duration)
        {
            main_cam.transform.position = Vector3.Lerp(orig_pos, up_pos, elapsed / up_duration);
            main_cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(orig_rot, Vector3.zero, elapsed / up_duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        Vector3 fwd_pos = up_pos;
        fwd_pos.z = 8;
        elapsed = 0;
        float fwd_duration = 0.5f;
        while(elapsed < up_duration)
        {
            main_cam.transform.position = Vector3.Lerp(up_pos, fwd_pos, elapsed / fwd_duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(transition_UI.GetComponent<SceneTransition>().FadeOut(3f, level));
        //EventBus.Publish(new LoadWorldEvent(level));
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
<<<<<<< HEAD
=======

    public float GetSensitivity()
    {
        return sensitivity;
    }

    public void SetSensitivity(float _in)
    {
        sensitivity = _in;
    }

    IEnumerator EaseIn(RectTransform panel)
    {
        // Ease In the UI panel
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / ease_duration;

        while(progress < 1.0f)
        {
            progress = (Time.time - initial_time) / ease_duration;
            float eased_progress = ease.Evaluate(progress);
            panel.anchoredPosition = Vector3.LerpUnclamped(hidden_pos, visible_pos, eased_progress);

            yield return null;
        }
    }

    IEnumerator EaseOut(RectTransform panel)
    {
        // Ease Out the UI panel
        float initial_time = Time.time;
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / ease_duration;
            float eased_progress = ease_out.Evaluate(progress);
            panel.anchoredPosition = Vector3.LerpUnclamped(hidden_pos, visible_pos, 1.0f - eased_progress);

            yield return null;
        }
    }
>>>>>>> 26d0d511e5b79e954459842ed519e008ee914909
}
