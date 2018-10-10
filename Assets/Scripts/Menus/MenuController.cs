using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject achievementPanel;

    [SerializeField] private AudioListener audioListener;
    [SerializeField] private PlayerController playerController;

    bool isMenuOpened = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

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

    public void MenuOpen()
    {
        menuObject.SetActive(true);
        Time.timeScale = 0;
        audioListener.enabled = false;
        //To add Input change (to disabled)
    }

    public void MenuClose()
    {
        menuObject.SetActive(false);
        Time.timeScale = 1;
        audioListener.enabled = true;
        //To add Input change (to disabled)
    }


    #region AchievementPanel
    public void OpenAchievementsMenu()
    {
        menuObject.SetActive(false);
        achievementPanel.SetActive(true);
    }

    public void CloseAchievementsMenu()
    {
        menuObject.SetActive(true);
        achievementPanel.SetActive(false);
    }
    #endregion

    public void OpenMainMenu()
    {
        //to add
    }
}
