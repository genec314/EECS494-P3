using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialControl3 : MonoBehaviour
{
    public RectTransform tutorialUI;

    // The two places the toast UI panel alternates between.
    public Vector3 hidden_pos = new Vector3(0f, -200f, 0f);
    public Vector3 visible_pos = new Vector3(0f, 0f, 0f);

    // These inspector-accessible variables control how the toast UI panel moves between the hidden and visible positions.
    public AnimationCurve ease;
    public AnimationCurve ease_out;

    // Duration controls.
    public float ease_duration = 0.5f;
    GameControl gc;
    Subscription<LevelStartEvent> start_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
        gc = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    void Update()
    {
        if (gc.tutorial_hole4 && Input.GetKeyDown(KeyCode.F) && !gc.tutorial_firstF)
        {
            gc.tutorial_firstF = true;
            tutorialUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press F to rejoin the split balls at any time before the end of your last shot!";
        } else if (gc.tutorial_firstF && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(EndTutorial());
        }
    }

    private void StartLevel(LevelStartEvent e)
    {
        if (!gc.tutorial_hole4 && e.level.GetLevelNumber() == 3)
        {
            gc.tutorial_hole4 = true;
            StartCoroutine(EaseIn(tutorialUI));
            tutorialUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Try shooting straight, and press F to split the ball while the ball is moving!";
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(EaseOut(tutorialUI));
        yield return new WaitForSeconds(0.5f);
        tutorialUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Use it wisely, you can only split the ball once per level!";
        StartCoroutine(EaseIn(tutorialUI));
        yield return new WaitForSeconds(3f);
        StartCoroutine(EaseOut(tutorialUI));
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
}
