using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    [SerializeField] private int sceneID;

    [SerializeField] private Vector2 loadDestination;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.transform.position = loadDestination;

            if (SceneManager.GetActiveScene().buildIndex != sceneID)
                SceneManager.LoadScene(sceneID);           
        }
    }

    
}
