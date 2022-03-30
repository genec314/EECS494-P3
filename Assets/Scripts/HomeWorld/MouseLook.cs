using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    private float sensitivity = 100f;

    HomeWorldControl hwc;
    Transform playerTf;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTf = GameObject.Find("Player").GetComponent<Transform>();

        hwc = GameObject.Find("HomeWorldManager").GetComponent<HomeWorldControl>();
        if(hwc != null)
        {
            sensitivity = Mathf.Max(hwc.GetSensitivity() * 500, 100);
        }
        else
        {
            sensitivity = 500f;
        }
        sensitivity = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hwc.CanFreeMove()){
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        this.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTf.Rotate(Vector3.up * mouseX);        
    }
}
