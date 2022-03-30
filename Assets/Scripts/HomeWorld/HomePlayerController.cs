using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePlayerController : MonoBehaviour
{
    Subscription<TutorialStrikeEvent> tutorial_strike_sub;

    PlayerMovement pm;
    CharacterController cc;
    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        cc = GetComponent<CharacterController>();
        camera = transform.Find("FirstPersonCam").gameObject;

        tutorial_strike_sub = EventBus.Subscribe<TutorialStrikeEvent>(_OnTutorialStrike);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void _OnTutorialStrike(TutorialStrikeEvent e)
    {

        StartCoroutine(SetupFreeMove());

    }

    IEnumerator SetupFreeMove()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(0, 0.5f, -20f);
        camera.SetActive(true);
        camera.transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pm.enabled = true;
        cc.enabled = true;
    }
}
