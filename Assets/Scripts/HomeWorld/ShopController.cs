using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopController : MonoBehaviour
{

    public GameObject[] balls;

    public Image arrow;
    public int num_balls = 4;
    
    struct Ball
    {
        public bool purchased;
        public int cost;
        public string color;

        public Ball(int _cost, string _color)
        {
            purchased = false;
            cost = _cost;
            color = _color;
        }

    }

    int cur_ball = 0;
    Ball[] inventory;
    static ShopController sc;
    // Start is called before the first frame update
    void Start()
    {
        if(sc != null)
        {
            Destroy(this.gameObject);
            return;
        }
        sc = this;
        DontDestroyOnLoad(this);

        inventory = new Ball[balls.Length];

        inventory[0] = new Ball(100, "Red");
        inventory[1] = new Ball(200, "Blue");
        inventory[2] = new Ball(500, "Yellow");
        inventory[3] = new Ball(1000, "Purple");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && cur_ball < 3)
        {
            cur_ball += 1;
            arrow.transform.SetParent(balls[cur_ball].transform);
            arrow.transform.localPosition = new Vector3(-120, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && cur_ball > 0)
        {
            cur_ball -= 1;
            arrow.transform.SetParent(balls[cur_ball].transform);
            arrow.transform.localPosition = new Vector3(-120, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inventory[cur_ball].purchased)
            {
                return;
            }
            balls[cur_ball].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
            inventory[cur_ball].purchased = true;
            GameObject pin = balls[cur_ball].transform.Find("Pin").gameObject;
            pin.SetActive(false);

        }
    }
}
