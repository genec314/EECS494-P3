using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThrowCounter : MonoBehaviour
{
    public int max_throws = 99;
    int throws_count = 0;
    Text text;
    
    Subscription<BallThrownEvent> thrown_sub;
    Subscription<BallAtRestEvent> rest_sub;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        thrown_sub = EventBus.Subscribe<BallThrownEvent>(UpdateThrownCount);
        rest_sub = EventBus.Subscribe<BallAtRestEvent>(CheckReloadLevel);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallThrownEvent>(thrown_sub);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Throws: " + throws_count;   
    }

    void UpdateThrownCount(BallThrownEvent e)
    {
        throws_count++;
    }

    void CheckReloadLevel(BallAtRestEvent e)
    {
        if (throws_count >= max_throws) EventBus.Publish<ReloadLevelEvent>(new ReloadLevelEvent());
    }
}
