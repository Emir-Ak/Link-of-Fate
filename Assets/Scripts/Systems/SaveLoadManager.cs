using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;

public static class SaveLoadManager {

    static string gameSavingPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources) + @"/Link Of Fate/Saves/";
    static string achievementsSavingPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources) + @"/Link Of Fate/Achievements/";

    public enum SavingSlot
    {
        Slot1,
        Slot2,
        Slot3
    }

    public enum SavingType
    {
        PlayerSave,
        DefaultSave
    }

    [Serializable]
    private struct _Achievement
    {
        [SerializeField] internal string name, description;
        [SerializeField] internal string achievementImageName;
        [SerializeField] internal int progress, progressGoal;
        [SerializeField] internal bool isCompleted;
    }

    struct AchievementWrapper
    {
        public _Achievement[] achievements;
    }

    public static void SaveAchievements(SavingType type)
    {
        string SavingDirectory = achievementsSavingPath;
       
        switch (type)
        {
            case SavingType.PlayerSave:
                SavingDirectory += "Achievement_Save";
                break;
            case SavingType.DefaultSave:
                SavingDirectory += "Default_Achievemen_Save";
                Debug.Log(SavingDirectory);
                break;
            default:
                break;
        }

        Achievement[] achievementArray = AchievementManager.achivementDictionary.Values.ToArray();
        _Achievement[] achievementWrapper = new _Achievement[achievementArray.Length];

        for (int i = 0; i < achievementArray.Length; i++)
        {
            achievementWrapper[i].name = achievementArray[i].name;
            achievementWrapper[i].description = achievementArray[i].description;
            achievementWrapper[i].achievementImageName = achievementArray[i].achievementImage.name;
            achievementWrapper[i].progress = achievementArray[i].progress;
            achievementWrapper[i].progressGoal = achievementArray[i].progressGoal;
            achievementWrapper[i].isCompleted = achievementArray[i].isCompleted;
        }

        string outputString = JsonUtility.ToJson(new AchievementWrapper() { achievements = achievementWrapper });
        Debug.Log(outputString);
        File.WriteAllText("C:\\Users\\Lenovo\\Desktop\\MyFile.json", outputString);
    }

    public static void LoadAchievements()
    {
        AchievementManager.achivementDictionary["name"].isCompleted = true;
    }

    public static void SaveSlot(SavingSlot slot)
    {
        switch (slot)
        {
            case SavingSlot.Slot1:
                break;
            case SavingSlot.Slot2:
                break;
            case SavingSlot.Slot3:
                break;
            default:
                break;
        }
    }

    public static void LoadSlot(SavingSlot slot)
    {
        switch (slot)
        {
            case SavingSlot.Slot1:
                break;
            case SavingSlot.Slot2:
                break;
            case SavingSlot.Slot3:
                break;
            default:
                break;
        }
    }

}
