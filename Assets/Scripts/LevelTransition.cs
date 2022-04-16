using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public Animator transition;

    Subscription<LevelTransitionEvent> transition_sub;

    // Start is called before the first frame update
    void Start()
    {
        transition_sub = EventBus.Subscribe<LevelTransitionEvent>(DoTransition);
    }

    void DoTransition(LevelTransitionEvent e)
    {
        transition.SetTrigger("Start");
    }
}
