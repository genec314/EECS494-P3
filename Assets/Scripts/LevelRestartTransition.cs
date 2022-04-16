using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestartTransition : MonoBehaviour
{
    public CircleWipeController transition;

    Subscription<LevelRestartTransitionEvent> transition_sub;

    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponent<CircleWipeController>();
        transition_sub = EventBus.Subscribe<LevelRestartTransitionEvent>(DoTransition);
    }

    void DoTransition(LevelRestartTransitionEvent e)
    {
        transition.FadeInAndOut();
    }
}
