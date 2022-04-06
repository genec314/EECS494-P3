using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCamPosAndRotation : MonoBehaviour
{
    Transform tf;
    Subscription<LevelStartEvent> start_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
    }

    private void StartLevel(LevelStartEvent e)
    {
        // add the lerp eventually
        tf.position = e.level.GetInitialCameraPosition();

        // set camera rotation to (0, 0, 0) then update
        tf.rotation = Quaternion.identity;
        tf.Rotate(e.level.GetInitialCameraRotation());
    }

}
