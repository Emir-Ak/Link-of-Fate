using UnityEngine;

public class DestroyItem : MonoBehaviour {

    public float delayTime;
    [Header("Empty will mean current GameObject")]
    public GameObject objToDestroy;
	void Start () {
        if (objToDestroy == null)
        {
            objToDestroy = gameObject;
        }
        Destroy(objToDestroy, delayTime);
	}

}
