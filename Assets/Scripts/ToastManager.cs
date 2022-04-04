using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    public bool is_complete = false;
    public bool is_failed = false;

    // The two places the toast UI panel alternates between.
    public Vector3 hidden_pos = new Vector3(20f, 200f, 0f);
    public Vector3 visible_pos = new Vector3(20f, -200f, 0f);

    // These inspector-accessible variables control how the toast UI panel moves between the hidden and visible positions.
    public AnimationCurve ease;
    public AnimationCurve ease_out;

    // Duration controls.
    public float ease_duration = 0.5f;
    public float show_duration = 1.5f;
    
    Subscription<EndHoleEvent> complete_sub;
    Subscription<LevelFailedEvent> fail_sub;

    // Start is called before the first frame update
    void Start()
    {
        complete_sub = EventBus.Subscribe<EndHoleEvent>(OnLevelComplete);
        fail_sub = EventBus.Subscribe<LevelFailedEvent>(OnLevelFailed);
    }

    void OnLevelComplete(EndHoleEvent e)
    {
        if (is_complete) StartCoroutine(DisplayToast());
    }

    void OnLevelFailed(LevelFailedEvent e)
    {
        if (is_failed) StartCoroutine(DisplayToast());
    }

    IEnumerator DisplayToast()
    {
        // Ease In the UI panel
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / ease_duration;

        while(progress < 1.0f)
        {
            progress = (Time.time - initial_time) / ease_duration;
            float eased_progress = ease.Evaluate(progress);
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.LerpUnclamped(hidden_pos, visible_pos, eased_progress);

            yield return null;
        }

        // Show the UI Panel for "duration_show_sec" seconds.
        yield return new WaitForSeconds(show_duration);

        // Ease Out the UI panel
        initial_time = Time.time;
        progress = 0.0f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / ease_duration;
            float eased_progress = ease_out.Evaluate(progress);
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.LerpUnclamped(hidden_pos, visible_pos, 1.0f - eased_progress);

            yield return null;
        }
    }
}
