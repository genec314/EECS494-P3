// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class ScoreboardUI : MonoBehaviour
// {
//     Vector3 left;
//     Vector3 right;

//     public GameObject background;
//     public GameObject background2;
//     public GameObject border;
//     public GameObject blackLinePrefab;
//     public GameObject strikeText;
//     public GameObject spareText;
//     public GameObject pinsText;
//     public GameObject scoreTextPrefab;

//     private bool strikeOnCurrentHole = false;
//     private bool spareOnCurrentHole = false;
//     private bool strikeOnLastHole = false;
//     private bool strikeBeforeLastHole = false;
//     private bool spareOnLastHole = false;
//     private int[] scores = new int[11] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
//     private GameObject[] textScores = new GameObject[11];

//     private int totalScore;
//     private int currentHoleScore;

//     Transform tf;
//     Subscription<EndHoleEvent> end_hole_subscription;

//     [SerializeField] int numHoles;

//     // Start is called before the first frame update
//     void Awake()
//     {
//         RectTransform rt = background.GetComponent<RectTransform>();
//         tf = this.GetComponent<Transform>();

//         end_hole_subscription = EventBus.Subscribe<EndHoleEvent>(EndHole);

//         left = new Vector3(-rt.rect.width / 2, -65, 0);
//         right = new Vector3(rt.rect.width / 2, -65, 0);

//         float partition = (rt.rect.width) / (numHoles+1);

//         for (int i = 0; i < numHoles; ++i)
//         {
//             GameObject blackLine = Instantiate(blackLinePrefab, new Vector3(0, 0, 0), Quaternion.identity, background.GetComponent<RectTransform>());
//             RectTransform brt = blackLine.GetComponent<RectTransform>();

//             brt.localPosition = new Vector3(left.x + (i + 1) * partition, 0, 0);
//         }

//         for (int i = 0; i < numHoles + 1; ++i)
//         {
//             GameObject scoreText = Instantiate(scoreTextPrefab, new Vector3(0, 0, 0), Quaternion.identity, background.GetComponent<RectTransform>());
//             RectTransform srt = scoreText.GetComponent<RectTransform>();

//             srt.localPosition = new Vector3(left.x + (i + 1) * partition - (partition / 2), 0, 0);
//             textScores[i] = scoreText;
//         }
//     }

//     private void EndHole(EndHoleEvent e)
//     {
//         HoleData holeData = e.currentHole;

//         int[] scoresByFrame = holeData.GetScoresByShot();

//         currentHoleScore = 10 - holeData.GetPinsRemaining();

//         scores[holeData.GetHoleNumber() - 1] = currentHoleScore;

//         strikeBeforeLastHole = strikeOnLastHole;
//         strikeOnLastHole = strikeOnCurrentHole;
//         spareOnLastHole = spareOnCurrentHole;

//         strikeOnCurrentHole = false;
//         spareOnCurrentHole = false;

//         if (currentHoleScore == 10)
//         {
//             strikeOnCurrentHole = holeData.GetShotsTaken() == 1;

//             if (strikeOnCurrentHole)
//             {
//                 background2.SetActive(true);
//                 border.SetActive(true);
//                 strikeText.SetActive(true);
//                 StartCoroutine(ShowText(strikeText));
//             }

//             spareOnCurrentHole = holeData.GetShotsTaken() == 2;

//             if (spareOnCurrentHole)
//             {
//                 background2.SetActive(true);
//                 border.SetActive(true);
//                 spareText.SetActive(true);
//                 StartCoroutine(ShowText(spareText));
//             }
//         }

        
//         int pinsGot = 10 - holeData.GetPinsRemaining();

//         if (pinsGot != 1 && !(strikeOnCurrentHole || spareOnCurrentHole))
//         {
//             pinsText.GetComponent<TextMeshProUGUI>().text = "You got " + pinsGot + " pins!";
//             background2.SetActive(true);
//             border.SetActive(true);
//             pinsText.SetActive(true);
//             StartCoroutine(ShowText(pinsText));
//         } else if (pinsGot == 1)
//         {
//             pinsText.GetComponent<TextMeshProUGUI>().text = "You got " + pinsGot + " pin!";
//             background2.SetActive(true);
//             border.SetActive(true);
//             pinsText.SetActive(true);
//             StartCoroutine(ShowText(pinsText));
//         }

//         if (strikeBeforeLastHole)
//         {
//             if (strikeOnLastHole)
//             {
//                 // add score from first shot of this hole
//                 scores[holeData.GetHoleNumber() - 3] += holeData.GetScoresByShot()[0];
//             }
//         }

//         if (strikeOnLastHole)
//         {
//             if (strikeOnCurrentHole)
//             {
//                 scores[holeData.GetHoleNumber() - 2] += 10;
//             } else
//             {
//                 scores[holeData.GetHoleNumber() - 2] += holeData.GetScoresByShot()[0] + holeData.GetScoresByShot()[1];
//             }
//         }

//         if (spareOnLastHole)
//         {
//             scores[holeData.GetHoleNumber() - 2] += holeData.GetScoresByShot()[0];
//         }

//         int sum = 0;

//         // update actual scoreboard
//         for (int i = 0; i < holeData.GetHoleNumber(); ++i)
//         {
//             sum += scores[i];
//             textScores[i].GetComponent<TextMeshProUGUI>().text = scores[i].ToString();
//         }

//         // HARDCODED FOR NOW
//         textScores[10].GetComponent<TextMeshProUGUI>().text = sum.ToString();
//     }

//     IEnumerator ShowText(GameObject gameObject)
//     {
//         yield return new WaitForSeconds(2f);
//         gameObject.SetActive(false);
//         background2.SetActive(false);

//         background.SetActive(true);
//         yield return new WaitForSeconds(2f);
//         background.SetActive(false);
//         border.SetActive(false);
//     }
// }
