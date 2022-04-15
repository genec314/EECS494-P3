using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorBall : MonoBehaviour
{

    Subscription<LevelCompleteEvent> level_complete_sub;

    Rigidbody rb;

    float small_force = 0f;
    float big_force = 250f;

    bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        string path = "Materials/Balls/";
        Object[] mats = Resources.LoadAll(path);
        GetComponent<MeshRenderer>().material = (Material)mats[Random.Range(0, mats.Length)];

        small_force = Random.Range(0f, 100f);
        big_force = Random.Range(200f, 350f);
        rb = GetComponent<Rigidbody>();
        level_complete_sub = EventBus.Subscribe<LevelCompleteEvent>(_OnLevelComplete);

        Jump(small_force);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void _OnLevelComplete(LevelCompleteEvent e)
    {
        jumping = true;
        Jump(big_force);
        StartCoroutine(StopJumping());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumping)
        {
            Jump(big_force);
        }
        else
        {
            //Jump(small_force);
        }
    }
    IEnumerator StopJumping()
    {
        yield return new WaitForSeconds(4f);
        jumping = false;
    }

    void Jump(float force)
    {
        rb.AddForce(new Vector3(0, force, 0));
    }
}
