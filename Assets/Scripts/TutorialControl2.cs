using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl2 : MonoBehaviour
{
    public GameObject tutorialUI;
    
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(EndTutorial());
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorialUI.SetActive(false);
    }
}
