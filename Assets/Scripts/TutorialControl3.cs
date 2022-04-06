using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialControl3 : MonoBehaviour
{
    public GameObject tutorialUI;

    private bool hole1 = true;
    private bool hole4 = false;
    private bool firstF = true;

    Subscription<LevelStartEvent> start_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        start_subscription = EventBus.Subscribe<LevelStartEvent>(StartLevel);
    }

    void Update()
    {
        if (hole1 && Input.GetKeyDown(KeyCode.Space))
        {
            hole1 = false;
            StartCoroutine(EndTutorial());
        } else if (hole4 && Input.GetKeyDown(KeyCode.F) && firstF)
        {
            firstF = false;
            tutorialUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press F to rejoin the split balls at any time before the end of your last shot!";
        } else if (!firstF && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(EndTutorial());
        }
    }

    private void StartLevel(LevelStartEvent e)
    {
        if (e.level.GetLevelNumber() == 3)
        {
            hole4 = true;
            tutorialUI.SetActive(true);
            tutorialUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Try shooting straight, and press F to split the ball while the ball is moving!";
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(3f);
        tutorialUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Use it wisely, you can only split the ball once per level!";
        yield return new WaitForSeconds(3f);
        tutorialUI.SetActive(false);
    }
}
