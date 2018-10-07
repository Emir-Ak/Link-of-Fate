using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameObject menuObject;

    [SerializeField] private AudioListener audioListener;

    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();    
    }

    void EscapeButton()
    {
        menuObject.SetActive(!menuObject.activeSelf);   
        Time.timeScale = Convert.ToInt32(menuObject.activeSelf);
        audioListener.enabled = menuObject.activeSelf;
        //To add Input change (to disabled)
    }
}
