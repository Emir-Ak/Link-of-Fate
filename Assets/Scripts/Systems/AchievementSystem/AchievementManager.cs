using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    

    //[SerializeField] private List<>

    public delegate void VoidDelegate();

    public static Dictionary<string, Achievement> achivementDictionary = new Dictionary<string, Achievement>();
    public Achievement[] achievements;

    private void Start()
    {


        foreach (Achievement achievement in achievements)
        {
            achivementDictionary.Add(achievement.name, achievement);
        }


        SaveLoadManager.SaveAchievements(SaveLoadManager.SavingType.DefaultSave);



        //this Stuff is going to be moved to SaveLoadManager



        //if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Link Of Fate\AchievementSaveFile.json"))
        //{
        //    //load achievements in
        //}
        //else
        //{
        //    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Link Of Fate"))
        //    {
        //        File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Link Of Fate");
        //    }
        //}

        /*
         *  else if(no save file)
         *  {
         *      initialize achievements from inspector instead.
         *      (or from a default file maybe, since there's already a loading feature why not use it to simplify code xD)
         *  }
         */
        //Saving Path MyDocuments/Link Of Fate as Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        //var inputString = File.ReadAllText("C:\\MyFile.json");
        //achievementArray = JsonUtility.FromJson<_Achievement[]>(inputString);
    }
}



[Serializable]
public class Achievement
{
    public string name, description;
    public Sprite achievementImage;
    public int progress, progressGoal;
    public bool isCompleted = false;


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