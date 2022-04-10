using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;

    Subscription<SceneTransitionEvent> transition_sub;

    // Start is called before the first frame update
    void Start()
    {
        transition_sub = EventBus.Subscribe<SceneTransitionEvent>(DoTransition);
    }

    void DoTransition(SceneTransitionEvent e)
    {
        transition.SetTrigger("Start");
    }
}
