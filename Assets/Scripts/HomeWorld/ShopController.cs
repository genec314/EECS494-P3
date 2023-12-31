using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopController : MonoBehaviour
{

    public GameObject[] balls;

    public Image arrow;
    public Image check;
    public int num_balls = 4;

    HomeWorldData hwd;
    PlayerInventory pi;
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

        hwd = GameObject.Find("GameControl").GetComponent<HomeWorldData>();
        pi = GameObject.Find("GameControl").GetComponent<PlayerInventory>();

        inventory = new Ball[balls.Length];

        inventory[0] = new Ball(0, "Red");
        inventory[1] = new Ball(50, "Gray");
        inventory[2] = new Ball(100, "Yellow");
        inventory[3] = new Ball(1000, "Purple");

        List<int> purchased_balls = hwd.GetPurchasedBalls();
        for(int i = 0; i < purchased_balls.Count; i++)
        {
            inventory[purchased_balls[i]].purchased = true;
            balls[purchased_balls[i]].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
            GameObject pin = balls[purchased_balls[i]].transform.Find("Pin").gameObject;
            pin.SetActive(false);
        }

        ChangeActiveBall(hwd.GetActiveBall());
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && cur_ball < 3)
        {
            cur_ball += 1;
            arrow.transform.SetParent(balls[cur_ball].transform);
            arrow.transform.localPosition = new Vector3(-120, 0, 0);
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && cur_ball > 0)
        {
            cur_ball -= 1;
            arrow.transform.SetParent(balls[cur_ball].transform);
            arrow.transform.localPosition = new Vector3(-120, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inventory[cur_ball].purchased)
            {
                ChangeActiveBall(cur_ball);

                return;
            }
            if(pi.GetPins() >= inventory[cur_ball].cost)
            {
                EventBus.Publish(new BallBoughtEvent(cur_ball, inventory[cur_ball].cost));
                balls[cur_ball].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
                inventory[cur_ball].purchased = true;
                GameObject pin = balls[cur_ball].transform.Find("Pin").gameObject;
                pin.SetActive(false);

                ChangeActiveBall(cur_ball);
            }
        }
    }

    void ChangeActiveBall(int index)
    {
        check.gameObject.SetActive(true);
        check.transform.SetParent(balls[cur_ball].transform);
        check.transform.localPosition = new Vector3(130, 0, 0);

        MeshRenderer ball = GameObject.Find("Player").GetComponent<MeshRenderer>();
        string path = "Materials/Balls/" + inventory[index].color;
        Material mat = (Material)Resources.Load(path);
        ball.material = mat;

        hwd.SetActiveBall(index);
    }
}
