using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public static GameObject mainCamera;

    public Transform playerTransform; //Public variable to store a reference to the player game object
    public float lerpSpeed = 1f;

    [SerializeField] private Vector2 offset; //Private variable to store the offset distance between the player and camera

    private void Awake()
    {
        if (mainCamera == null)
        {
            DontDestroyOnLoad(gameObject);
            mainCamera = this.gameObject;
        }
        else if (mainCamera != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        ////Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - playerTransform.position;
        //Makes things harder in the Scene, the Offset should be set manually (plus who would want that anyway xD?)
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (playerTransform != null)
        {          
            transform.position = Vector2.Lerp(transform.position, (Vector2)playerTransform.position + offset, Time.deltaTime * lerpSpeed);
        }
    }
}