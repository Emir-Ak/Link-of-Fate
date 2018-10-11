using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

	[SerializeField] GameObject creditPanel;
	[SerializeField] GameObject textObject;

    RectTransform textTransform;
	public float lerpSpeed;

	private float textHeight;
	Vector2 startPos;
	Vector2 endPos;

	// Use this for initialization
	void Start ()
	{
        textTransform = textObject.GetComponent<RectTransform>();
        textHeight = textTransform.sizeDelta.y;
		startPos = new Vector3(0, creditPanel.GetComponent<RectTransform>().sizeDelta.y); ;
		endPos = new Vector3(0,-textHeight,0);
        StartCoroutine(Lerp(endPos,20));
    }


    IEnumerator Lerp(Vector3 toPos, float time)
    {
        float delta = 0;
        Vector3 original = textTransform.anchoredPosition;
        while (delta < time)
        {
            delta += Time.deltaTime;
            textTransform.anchoredPosition = Vector3.Lerp(original, toPos, delta / time);
            yield return null;
        }
    }
}
