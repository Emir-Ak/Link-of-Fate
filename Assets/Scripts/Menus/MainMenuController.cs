using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject CreditsPanel;




    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject StartNewButton;



    #region AchievementPanel
    public void OpenAchievementsMenu()
    {
        mainMenuPanel.SetActive(false);
        achievementPanel.SetActive(true);
    }

    public void CloseAchievementsMenu()
    {
        mainMenuPanel.SetActive(true);
        achievementPanel.SetActive(false);
    }
    #endregion

    #region Options
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }
    #endregion

    #region Credits
    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        mainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
    #endregion

    public void StartNewGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        Application.Quit();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
