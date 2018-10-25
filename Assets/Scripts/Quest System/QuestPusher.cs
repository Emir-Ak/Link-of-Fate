using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test script
public class QuestPusher : MonoBehaviour {

    [Header("TEST SCRIPT")]
    public Quest quest;
    QuestManager questManager;

    public float acceptDelayTime = 5f;
    public float abandonDelayTime = 40f;
   
    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }
    private void Start()
    {
        Invoke("AcceptQuest", acceptDelayTime);
        Invoke("AbandonQuest", abandonDelayTime);
    }

    void AcceptQuest()
    {
        questManager.AcceptQuest(quest);
    }
    void AbandonQuest()
    {
        questManager.AbandonQuest(quest);
    }
}
