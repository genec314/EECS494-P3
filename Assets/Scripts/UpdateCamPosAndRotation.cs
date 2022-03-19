using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCamPosAndRotation : MonoBehaviour
{
    Transform tf;
    Subscription<NewHoleEvent> new_hole_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        new_hole_subscription = EventBus.Subscribe<NewHoleEvent>(NewHole);
    }

    private void NewHole(NewHoleEvent e)
    {
        // add the lerp eventually
        tf.position = e.nextHole.GetInitialCameraPosition();

        // set camera rotation to (0, 0, 0) then update
        tf.rotation = Quaternion.identity;
        tf.Rotate(e.nextHole.GetInitialCameraRotation());
    }

}
