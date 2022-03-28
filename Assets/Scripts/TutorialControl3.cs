using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialControl3 : MonoBehaviour
{
    public GameObject tutorialUI;

    private bool hole1 = true;
    private bool hole3 = false;
    private bool firstF = true;

    Subscription<NewHoleEvent> new_hole_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        new_hole_subscription = EventBus.Subscribe<NewHoleEvent>(NewHole);
    }

    void Update()
    {
        if (hole1 && Input.GetKeyDown(KeyCode.Space))
        {
            hole1 = false;
            StartCoroutine(EndTutorial());
        } else if (hole3 && Input.GetKeyDown(KeyCode.F) && firstF)
        {
            firstF = false;
            tutorialUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press F to rejoin the split balls!";
        } else if (!firstF && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(EndTutorial());
        }
    }

    private void NewHole(NewHoleEvent e)
    {
        Debug.Log(e.nextHole.GetHoleNumber());
        if (e.nextHole.GetHoleNumber() == 3)
        {
            hole3 = true;
            tutorialUI.SetActive(true);
            tutorialUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Trying shooting straight, and press F to split the ball!";
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(2f);
        tutorialUI.SetActive(false);
    }
}
