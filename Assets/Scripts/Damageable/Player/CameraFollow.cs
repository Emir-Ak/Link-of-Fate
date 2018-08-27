using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform playerTransform;       //Public variable to store a reference to the player game object
    public float lerpSpeed = 1f;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - playerTransform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (playerTransform != null)
        {          

            transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, Time.deltaTime * lerpSpeed);
        }
    }
}