using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapController : MonoBehaviour
{

    public Image[] pictures;
    public Image lock_pic;
    public GameObject to_unlock;

    int cur_image = 0;
    List<int> unlocked_worlds;
    List<int> world_costs;
    HomeWorldData hwd;
    // Start is called before the first frame update
    void Start()
    {
        hwd = GameObject.Find("HomeWorldManager").GetComponent<HomeWorldData>();
 
        pictures[0].enabled = true;
        for(int i = 1; i < pictures.Length; i++)
        {
            pictures[i].enabled = false;
        }

        unlocked_worlds = hwd.GetUnlockedWorlds();
        world_costs = new List<int>();
        world_costs.Add(10);
        world_costs.Add(80);
        world_costs.Add(200);
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && cur_image < (pictures.Length - 1))
        {
            ChangeImage(cur_image + 1);
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && cur_image > 0)
        {
            ChangeImage(cur_image - 1);
        }
    }

    void ChangeImage(int num)
    {
        pictures[cur_image].enabled = false;
        pictures[num].enabled = true;
        cur_image = num;

        if (unlocked_worlds.Contains(num))
        {
            lock_pic.enabled = false;
            to_unlock.SetActive(false);
        }
        else
        {
            lock_pic.enabled = true;
            to_unlock.SetActive(true);
            to_unlock.GetComponentInChildren<TextMeshProUGUI>().text = "x" + world_costs[num];
        }
    }
}
