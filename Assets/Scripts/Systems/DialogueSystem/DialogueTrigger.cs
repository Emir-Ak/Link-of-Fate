using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[System.Serializable]
public class DialogueTrigger : MonoBehaviour {


    [SerializeField]
    private GameObject dialogueBox;

    public Dialogue dialogue;
    
    [Header("Leave null if no quest/items given")]
    public Quest questToAccept = null;
    public Item[] itemsToGive = null;
    public Item[] itemsToTake = null;


    PlayerController player;

    public UnityEngine.Events.UnityEvent OnDialogueEnd;

    public bool repeatable = true;

    private bool isFirstTime = true;

    public bool isCutscene = false;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void TriggerDialogue()
    {
        if (repeatable||isFirstTime)
        {
            player.shouldFreeze = true;
            var dialogueManager = Instantiate(dialogueBox, transform.position, Quaternion.identity).GetComponentInChildren<DialogueManager>();

            dialogueManager.StartDialogue(dialogue, this);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player") && FindObjectOfType<DialogueManager>() == null)
        {
            if (isCutscene)
            {
                TriggerDialogue();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                TriggerDialogue();
            }
           
        }
    }

    public void EndDialogue()
    {
        player.shouldFreeze = false;
        if (isFirstTime)
        {
            if (questToAccept != null)
            {
                FindObjectOfType<QuestManager>().AcceptQuest(questToAccept);
            }

            Inventory inv = FindObjectOfType<Inventory>();
            if (itemsToGive != null)
            {
               
                foreach (Item itemToGive in itemsToGive)
                {
                    inv.AddItem(itemToGive);
                }
            }

            if (itemsToTake != null)
            {
                foreach (Item itemToTake in itemsToTake)
                {
                    inv.RemoveItem(itemToTake);
                }

            }
            isFirstTime = false;
            OnDialogueEnd.Invoke();
        }
    }


}
