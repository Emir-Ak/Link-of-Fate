using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    //Quests currently running 
    public List<Quest> runningQuests = new List<Quest>();
    //Dictionary of the quest names next to the way of completion, all referenced ways represented by int will be written down later
    public Dictionary<string, int> completedQuests = new Dictionary<string, int>();

    //Adds a quest to the list of taken quests
    public void AcceptQuest(Quest acceptedQuest)
    {
        Debug.Log(acceptedQuest.questName + "is accepted");
        runningQuests.Add(acceptedQuest);
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
            }
        }
    }

    //Adds a quest to completed quests
    public void CompleteQuest(string questName, int wayOfCompletion)
    {
        Debug.Log(questName + " is completed!");
        completedQuests.Add(questName, wayOfCompletion);
    }
}
