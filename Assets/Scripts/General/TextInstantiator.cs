using UnityEngine.UI;
using UnityEngine;
using System.Collections;
public class TextInstantiator : MonoBehaviour
{
    [SerializeField]
    GameObject textPrefab;
    [SerializeField]
    GameObject persistentCanvas;
    [SerializeField]
    Transform textGoal;

    public float textMoveSpeed = 10f;

    Text textToInstantiate;

    public void InstantiateText(string text, Vector3 instPos, Color32 color, float y_Offset = +40f)
    {
        instPos = new Vector3(0, instPos.y + y_Offset, 0f);

        GameObject textInstance = Instantiate(textPrefab, instPos, Quaternion.identity) as GameObject;

        textInstance.transform.SetParent(persistentCanvas.transform, false);
        textToInstantiate = textInstance.GetComponent<Text>();
        textToInstantiate.text = text;
        textToInstantiate.color = color;

        StartCoroutine(AnimatePosition(textInstance));
    }

    IEnumerator AnimatePosition(GameObject obj)
    {
        while (obj != null)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, textGoal.position, Time.deltaTime * textMoveSpeed); ;
            yield return new WaitForFixedUpdate();
        }
    }
}

