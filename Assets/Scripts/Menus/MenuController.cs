using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameObject menuObject;

    [SerializeField] private AudioListener audioListener;

    bool isMenuOpened = false;

    private void Update()
    {

        if (Input.GetKeyDown((KeyCode)PlayerControlKeys.MenuKey))
        {
            isMenuOpened = !isMenuOpened;
            if (isMenuOpened)
            {
                MenuClose();
            }
            else
            {
                MenuOpen();
            }
        }
    }

    void MenuOpen()
    {
        menuObject.SetActive(true);
        Time.timeScale = 0;
        audioListener.enabled = false;
        //To add Input change (to disabled)
    }

    void MenuClose()
    {
        menuObject.SetActive(false);
        Time.timeScale = 1;
        audioListener.enabled = true;
        //To add Input change (to disabled)
    }
}
