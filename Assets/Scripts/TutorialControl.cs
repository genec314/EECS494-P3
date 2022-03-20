using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour
{

    Subscription<BallThrownEvent> ball_thrown_sub;
    Subscription<PinKnockedOverEvent> pin_knocked_sub;

    GameObject UI;
    GameObject cube;

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
        UI = GameObject.Find("TutorialUI");
        cube = GameObject.Find("ChargedCube");
        UI.SetActive(false);
        cube.SetActive(false);
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

    void SetNextStage()
    {
        pins_down = 0;
        attempts = 0;
        cube.SetActive(true);
        UI.SetActive(true);
    }

}
