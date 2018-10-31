using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    //Quests currently running 
    public List<Quest> runningQuests = new List<Quest>();

    //Dictionary of the quest names next to the way of completion, all referenced ways represented by int will be written down later
    public Dictionary<string, int> completedQuests = new Dictionary<string, int>();
    //List of quest trackers to pass true to them if the corresponding quest is running
    public List<QuestTracker> questTrackers;

    //Adds a quest to the list of taken quests
    public void AcceptQuest(Quest acceptedQuest)
    {
        Debug.Log(acceptedQuest.questName + " is accepted");
        runningQuests.Add(acceptedQuest);
        foreach(QuestTracker questTracker in questTrackers)
        {
            if (questTracker.correspondingQuestName == acceptedQuest.questName)
            {
                questTracker.isRunning = true;
                questTracker.OnQuestAccept();
                break;
            }
        }
    }

    //Removes quest by referencing type of quest
    public void AbandonQuest(Quest abandonedQuest)
    {
        for (int i = 0; i < runningQuests.Count; i++)
        {
            if(runningQuests[i] == abandonedQuest)
            {
                Debug.Log(runningQuests[i].questName + " abandoned.");
                runningQuests.Remove(runningQuests[i]);
                break;
            }
        }

        foreach (QuestTracker questTracker in questTrackers)
        {
            if (questTracker.correspondingQuestName == abandonedQuest.questName)
            {
                questTracker.isRunning = false;
                questTracker.OnQuestAbandon();
                break;
            }
        }
    }

    //Removes quest by stating name of the quest
    public void AbandonQuest(string questName)
    {
        for (int i = 0; i < runningQuests.Count; i++)
        {
            if (runningQuests[i].questName == questName)
            {
                Debug.Log(runningQuests[i].questName + " abandoned.");
                runningQuests.Remove(runningQuests[i]);
                break;
            }          
        }
        foreach (QuestTracker questTracker in questTrackers)
        {
            if (questTracker.correspondingQuestName == questName)
            {
                questTracker.isRunning = false;
                questTracker.OnQuestAbandon();
                break;
            }
        }
    }

    //Adds a quest to completed quests
    public void CompleteQuest(string questName, int completionWay)
    {
       
        completedQuests.Add(questName, completionWay);

        for (int i = 0; i < runningQuests.Count; i++)
        {
            if (runningQuests[i].questName == questName)
            {
                Debug.Log(questName + " is completed! " + "Completion way: " + completionWay.ToString());
                runningQuests.Remove(runningQuests[i]);
                break;
            }            
        }

        foreach (QuestTracker questTracker in questTrackers)
        {
            if (questTracker.correspondingQuestName == questName)
            {
                questTracker.isRunning = false;
                questTracker.OnQuestComplete();
                break;
            }
        }
    }
}
