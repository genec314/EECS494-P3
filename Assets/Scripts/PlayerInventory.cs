using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    public GameObject UI;

    private int num_pins = 0;

    Subscription<BallBoughtEvent> ball_bought_sub;
    Subscription<GainPinsEvent> gain_pins_sub;
    // Start is called before the first frame update
    void Start()
    {
        ball_bought_sub = EventBus.Subscribe<BallBoughtEvent>(_OnBallBought);
        gain_pins_sub = EventBus.Subscribe<GainPinsEvent>(_OnGainPins);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen" || SceneManager.GetActiveScene().name == "Intro" || SceneManager.GetActiveScene().name == "Complete")
        {
            if (UI.activeSelf) UI.SetActive(false);
        }
        else
        {
            if (!UI.activeSelf) UI.SetActive(true);
        }
    }

    void _OnGainPins(GainPinsEvent e)
    {
        AddPins(e.num);
    }

    void _OnBallBought(BallBoughtEvent e)
    {
        AddPins(-e.cost);
    }

    public int GetPins()
    {
        return num_pins;
    }

    public void AddPins(int num)
    {
        num_pins += num;
        updateUI();
    }

    public void SetPins(int num)
    {
        num_pins = num;
        updateUI();
    }

    void updateUI()
    {
        UI.GetComponentInChildren<TextMeshProUGUI>().text = num_pins.ToString();
    }
}
