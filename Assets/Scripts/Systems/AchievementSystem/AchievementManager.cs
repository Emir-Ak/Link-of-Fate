using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AchievementManager : MonoBehaviour
{
    public delegate void VoidDelegate();

    public static Dictionary<string, Achievement> achivementDictionary = new Dictionary<string, Achievement>();

    private void Start()
    {

    }
    /* Here the List of achievements will be made and initialized in the inspector.
     * Then it will be parsed into the dictionary for easy and precise access from any place
     * 
     */
}

public class Achievement
{

    /* This is what will be here:
     * name, description, image (everything for the UI)
     * + 
     * Needed trackers for the achievement (kill etc etc)
     * isCompleted bool
     */ 
    private string name = string.Empty;

    public static AchievementManager.VoidDelegate EnemyKilled;

    public static void EnemyKilledTrigger()
    {
        EnemyKilled();
        
    }

    private void Start()
    {
        EnemyKilled += KillCount;
    }

    private void KillCount()

    {
        Debug.Log("Killed enemy");
    }

    /*
     *  Dictionary<name,Achievement>
     *  {
     *     ("Goblin Slayer") Achievement
     *      {
     *          string name, ("Goblin Slayer")
     *          string description, ("Kill 100 Goblins")
     *          Image image (goblin image or something)
     *          trackableValues[]
     *          {
     *              tracker1();
     *          }
     *      }
     *  }
     */
}