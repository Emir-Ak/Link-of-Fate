using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessQuest : MonoBehaviour {

    [SerializeField]
    DialogueTrigger dialogueTrigger;
    [SerializeField]
    PrincessQuestController questController;

    public string[] fightD;
    public string[] moneyD;
    public string[] kidnapD;

    bool isStageActive = false;

    private void Update()
    {
        if (questController.action == 1 && isStageActive == false)
        {
            Fight();
            ChangeDialogue(fightD);
        }
        else if (questController.action == 2 && isStageActive == false)
        {
            Money();
            ChangeDialogue(moneyD);
        }
        else if (questController.action == 3 && isStageActive == false)
        {
            Kidnap();
            ChangeDialogue(kidnapD);
        }

        
    }

    private void ChangeDialogue(string[] stringArray)
    {
        dialogueTrigger.dialogue.sentences = stringArray;
    }

    void Fight()
    {
        Debug.Log("Fight");
        isStageActive = true;
    }
    void Money()
    {
        Debug.Log("Money");
        isStageActive = true;
    }
    void Kidnap()
    {
        Debug.Log("Kidnap");
        isStageActive = true;
    }


    

}
