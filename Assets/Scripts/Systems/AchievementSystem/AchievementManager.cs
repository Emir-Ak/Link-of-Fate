using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class AchievementManager : MonoBehaviour
{

    
    //[SerializeField] private List<>

    public delegate void VoidDelegate();

    public static Dictionary<string, Achievement> achivementDictionary = new Dictionary<string, Achievement>();

    public UnityEvent unityEvent = new UnityEvent();
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
    private string name,description = string.Empty;

    Image achievementImage;
    bool isCompleted = false;

    
    /* This is what will be here:
     * name, description, image (everything for the UI)
     * + 
     * Needed trackers for the achievement (kill etc etc)
     * isCompleted bool
     */ 


    //public static AchievementManager.VoidDelegate EnemyKilled;

    //public static void EnemyKilledTrigger()
    //{
    //    EnemyKilled();
        
    //}

    //private void Start()
    //{
    //    EnemyKilled += KillCount;
    //}

    //private void KillCount()
    //{
    //    Debug.Log("Killed enemy");
        
    //}

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

    //static void OnKilledSomething<T>(T)
    //{
    //    switch (T)
    //    {
    //        case Goblin:
    //            {
    //                GoblinKillCount++;
    //            }
    //    }
    //}

    
    
    //// somewhere in Player
    //public event Action<Enemy> onEnemyKilled;

    //void DealDamage()
    //{
    //    if (enemy.health < 0)
    //    {
    //        KillEnemy(enemy);
    //        onEnemyKilled?.Invoke(enemy);
    //    }
    //}

}