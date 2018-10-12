using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{

    [SerializeField] GameObject creditPanel;
    [SerializeField] GameObject textObject;

    RectTransform textTransform;
    public float lerpSpeed;
    [Range(1.0f, 3.0f)] public float speedMultiplier;
    float currentMultiplier;

    private float textHeight;
    private float creditsPanelHeight;
    Vector2 startPos;
    Vector2 endPos;

    float startTime;
    float dis;
    float disCovered;
    Vector3 original;

    private void OnEnable()
    {
        currentMultiplier = 1;

        textTransform = textObject.GetComponent<RectTransform>();

        textHeight = textTransform.sizeDelta.y;//Height of the text object
        creditsPanelHeight = creditPanel.GetComponent<RectTransform>().sizeDelta.y;//height of the panel (top to bottom)

        startPos = new Vector3(0, creditsPanelHeight);
        endPos = new Vector3(0, -textHeight, 0);

        textTransform.anchoredPosition = startPos;

        dis = (endPos - startPos).magnitude;
        original = textTransform.anchoredPosition;

        disCovered = 0;

        StopAllCoroutines();
        StartCoroutine(Lerp());
    }

    IEnumerator Lerp()
    {
        while (dis > disCovered)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentMultiplier = speedMultiplier;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                currentMultiplier = 1;
            }

            disCovered += lerpSpeed * currentMultiplier * Time.deltaTime;
            textTransform.anchoredPosition = Vector3.Lerp(original, endPos, disCovered / dis);
            yield return null;
        }
    }
}

