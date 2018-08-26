using UnityEngine.UI;
using UnityEngine;

public class TextInstantiator : MonoBehaviour
{
    [SerializeField]
    GameObject textPrefab;
    [SerializeField]
    GameObject persistentCanvas;

    Text textToInstantiate;

    public void InstantiateText(string text, Vector3 instPos, Color32 color, float y_Offset = +40f)
    {
        instPos = new Vector3(0, instPos.y + y_Offset, 0f);
        GameObject damageTextInstance = Instantiate(textPrefab, instPos, Quaternion.identity) as GameObject;
        damageTextInstance.transform.SetParent(persistentCanvas.transform, false);
        textToInstantiate = damageTextInstance.GetComponent<Text>();
        textToInstantiate.text = text;
        textToInstantiate.color = color;
    }

    
}

