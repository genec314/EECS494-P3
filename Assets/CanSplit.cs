using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSplit : MonoBehaviour
{

    bool isSplit = false;
    public GameObject secondBall;
    GameObject secondBall1;
    [SerializeField] float joinSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isSplit)
            {
                //Bring them back together
                GetComponent<Rigidbody>().velocity = (secondBall1.transform.position - transform.position) * joinSpeed ;
                secondBall1.GetComponent<Rigidbody>().velocity = (transform.position - secondBall1.transform.position) * joinSpeed;
                
            }
            else
            {
                isSplit = true;
                Vector3 vel = GetComponent<Rigidbody>().velocity;
                secondBall1 = Instantiate(secondBall);
                secondBall1.transform.position = transform.position - new Vector3(3, 0, 0);
                Rigidbody rb = secondBall1.GetComponent<Rigidbody>();
                //rb = GetComponent<Rigidbody>();
                rb.velocity = vel;
                GetComponent<Rigidbody>().velocity = vel;
                transform.position = transform.position + new Vector3(3, 0, 0);
                GetComponent<Rigidbody>().velocity = vel;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ball")
        {
            Destroy(secondBall1);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.enabled = false;
        }
    }
}
