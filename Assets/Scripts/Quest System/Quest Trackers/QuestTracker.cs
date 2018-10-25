using UnityEngine;
public class QuestTracker : MonoBehaviour
{
    public string correspondingQuestName;
    [HideInInspector]
    public bool isRunning = false;

    public virtual void OnQuestAccept() {}
    public virtual void OnQuestAbandon() {}

    protected QuestManager questManager;
    protected bool isCompleted = false;
    protected int completionWay = -1; // '-1' : not yet accomplished
}
