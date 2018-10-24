using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject{
    public string questName;
    public string questTitle;
    [TextArea(10, 9)]
    public string questDescription;
   //Uncomment if required (For example whenever you need to save completed and accepted quests, simply create string of IDs and save it)
   //int questID;
}

