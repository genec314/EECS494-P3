using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialControl : MonoBehaviour
{

    Subscription<BallThrownEvent> ball_thrown_sub;
    Subscription<PinKnockedOverEvent> pin_knocked_sub;
    Subscription<BallReadyEvent> ball_ready_subscription;

    GameObject UI;

    private int pins_down = 0;
    private int attempts = 0;

    GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            return;
        }
        instance = this.gameObject;
        ball_thrown_sub = EventBus.Subscribe<BallThrownEvent>(_OnBallThrown);
        pin_knocked_sub = EventBus.Subscribe<PinKnockedOverEvent>(_OnPinKnocked);
        ball_ready_subscription = EventBus.Subscribe<BallReadyEvent>(_OnBallReady);
        UI = GameObject.Find("TutorialUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnBallThrown(BallThrownEvent e)
    {
        UI.SetActive(false);
    }

    void _OnPinKnocked(PinKnockedOverEvent e)
    {
        pins_down++;
        if(pins_down >= 10)
        {
            SetNextStage();
        }
    }

    void _OnBallReady(BallReadyEvent e)
    {
        if (pins_down < 10)
        {
            StartCoroutine(ResetScene());
        }
    }

    void SetNextStage()
    {
        UI.SetActive(true);
        UI.GetComponentInChildren<TextMeshProUGUI>().text = "Hey, you're pretty good! But what about this magnetic challenge?";

        StartCoroutine(NextLevel(3f));
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
        UI.GetComponentInChildren<TextMeshProUGUI>().text = "Welcome to my bowling alley! Hold Space to Shoot!";
    }
}
