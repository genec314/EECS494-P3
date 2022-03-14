using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public string level1name = "RollBallLab";

    public void PlayLevel1()
    {
        SceneManager.LoadScene(level1name);
    }
}
