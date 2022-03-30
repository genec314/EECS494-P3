using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 12f;

    HomeWorldControl hwc;
    Rigidbody rb;
    CharacterController cc;
    private void Start()
    {
        hwc = GameObject.Find("HomeWorldManager").GetComponent<HomeWorldControl>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();

    }
    // Update is called once per frame
    void Update()
    {
        if (!hwc.CanFreeMove())
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * speed * Time.deltaTime);
    }
}
