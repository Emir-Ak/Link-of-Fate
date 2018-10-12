using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject loadingSlotsPanel;
    [SerializeField] private Image[] slotImages;


    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject startNewButton;



    private void Start()
    {
        
        foreach(Image image in slotImages)
        {
            if (image.sprite != null)
            {
                continueButton.SetActive(true);
            }
        }

        
    }

    #region AchievementPanel
    public void OpenAchievementsMenu()
    {
        /* This is only temporary code
         * This is what should be here:
         * 1. the data is loaded from the achievement saving file
         * 2. achievements are initialized and created in the content
        */
        mainMenuPanel.SetActive(false);
        achievementPanel.SetActive(true);
    }

    public void CloseAchievementsMenu()
    {
        mainMenuPanel.SetActive(true);
        achievementPanel.SetActive(false);
    }
    #endregion

    #region Load_Panel
    public void OpenLoadingSlots()
    {
        mainMenuPanel.SetActive(false);
        loadingSlotsPanel.SetActive(true);
    }

    public void CloseLoadingSlots()
    {
        mainMenuPanel.SetActive(true);
        loadingSlotsPanel.SetActive(false);
    }
    #endregion

    #region Options
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    #endregion

    #region Credits
    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    #endregion

    public void StartNewGame()
    {
        /* This is only temporary code
         * This is what should be here:
         * 1. Slot menu is opened
         * 2. In this case if you press on a slot it will start a new game 
         * (If the slot is occupied permission of overwrite will be asked)
         * 3. (in the scene from another script) the persistent objects are created and initialized from a default saved values 
         * (These will be values for new game state)
        */
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        /* This is only temporary code
         * This is what should be here:
         * 1. the data is loaded according to the last played slot (1,2 or 3)
         * 2. the scene is changed
         * 3. (in the scene from another script) the persistent objects are created and initialized
        */
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
