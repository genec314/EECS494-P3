using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThrowCounter : MonoBehaviour
{
    public int throws_count = 0;
    Text text;
    
    Subscription<BallThrownEvent> thrown_sub;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        thrown_sub = EventBus.Subscribe<BallThrownEvent>(UpdateThrownCount);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<BallThrownEvent>(thrown_sub);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Throws made: " + throws_count;   
    }

    void UpdateThrownCount(BallThrownEvent e)
    {
        throws_count++;
        //  if (throws_rem <= 0) EventBus.Publish<ReloadLevelEvent>(new ReloadLevelEvent()); 
    }
}
