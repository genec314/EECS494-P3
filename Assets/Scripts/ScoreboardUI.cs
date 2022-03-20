using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    Vector3 left;
    Vector3 right;

    public GameObject background;
    public GameObject border;
    public GameObject blackLinePrefab;
    public GameObject strikeText;

    private bool strikeOnCurrentHole = false;
    private bool spareOnCurrentHole = false;
    private bool strikeOnLastHole = false;
    private bool strikeBeforeLastHole = false;
    private bool spareOnLastHole = false;

    private int scoreOnLastHole;
    private int scoreBeforeLastHole;

    private int totalScore;
    private int currentHoleScore;

    Transform tf;
    Subscription<EndHoleEvent> end_hole_subscription;

    [SerializeField] int numHoles;

    // Start is called before the first frame update
    void Awake()
    {
        RectTransform rt = background.GetComponent<RectTransform>();
        tf = this.GetComponent<Transform>();

        end_hole_subscription = EventBus.Subscribe<EndHoleEvent>(EndHole);

        left = new Vector3(-rt.rect.width / 2, -65, 0);
        right = new Vector3(rt.rect.width / 2, -65, 0);

        float partition = (rt.rect.width) / (numHoles+1);

        for (int i = 0; i < numHoles; ++i)
        {
            GameObject blackLine = Instantiate(blackLinePrefab, new Vector3(0, 0, 0), Quaternion.identity, background.GetComponent<RectTransform>());
            RectTransform brt = blackLine.GetComponent<RectTransform>();

            brt.localPosition = new Vector3((left.x + (i+1)*partition), 0, 0);
        }
    }

    private void EndHole(EndHoleEvent e)
    {
        HoleData holeData = e.currentHole;

        currentHoleScore = 10 - holeData.GetPinsRemaining();

        if (currentHoleScore == 10)
        {
            strikeOnCurrentHole = holeData.GetShotsTaken() == 1;

            if (strikeOnCurrentHole)
            {
                background.SetActive(true);
                border.SetActive(true);
                strikeText.SetActive(true);
                StartCoroutine(ShowText(strikeText));
            }

            spareOnCurrentHole = !strikeOnCurrentHole && holeData.GetShotsTaken() == 2;

            if (!(strikeOnCurrentHole || spareOnCurrentHole))
            {
                // update score ui
            }
        }

        // Spares and Strikes
        if (holeData.GetHoleNumber() > 1)
        {
            if (strikeOnLastHole)
            {

            } else if (spareOnLastHole)
            {

            }
        }

        // Strike two holes ago
        if (holeData.GetHoleNumber() > 2 && strikeBeforeLastHole)
        {

        }
    }

    IEnumerator ShowText(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        background.SetActive(false);
        border.SetActive(false);
    }
}
