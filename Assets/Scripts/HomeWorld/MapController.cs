using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapController : MonoBehaviour
{

    public Image[] pictures;
    public Image lock_pic;

    int cur_image = 0;
    List<int> unlocked_worlds;

    HomeWorldData hwd;
    PlayerInventory pi;
    // Start is called before the first frame update
    void Start()
    {
        hwd = GameObject.Find("GameControl").GetComponent<HomeWorldData>();
 
        pictures[0].enabled = true;
        for(int i = 1; i < pictures.Length; i++)
        {
            pictures[i].enabled = false;
        }

        unlocked_worlds = hwd.GetUnlockedWorlds();
        lock_pic.enabled = false;

        pi = GameObject.Find("GameControl").GetComponent<PlayerInventory>();
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
        }
        else
        {
            lock_pic.enabled = true;
        }
    }
}
