﻿using UnityEngine;
public class PrincessQuestTracker : QuestTracker
{
    public int goblinGuardsToKillNum = 2;
    int goblinGuardsKilled = 0; //Need to kill 2

    public GameObject goblinGuards;

    void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        goblinGuards.SetActive(false);
    }



    public override void OnQuestAccept()
    {
        goblinGuards.SetActive(true);
    }

    public override void OnQuestAbandon()
    {
        goblinGuards.SetActive(false);
    }

    void CompleteQuest(int _completionWay)
    {
        completionWay = _completionWay;
        questManager.CompleteQuest(correspondingQuestName, completionWay);
    }

    public void KilledGoblinGuard()
    {
        Debug.Log("Killed a guard");
        goblinGuardsKilled++;
        if (goblinGuardsKilled >= goblinGuardsToKillNum)
        {
            CompleteQuest(1);
        }
    }

}
