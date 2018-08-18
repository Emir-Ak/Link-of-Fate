using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[System.Serializable]
public class DialogueTrigger : MonoBehaviour {


    [SerializeField]
    private GameObject dialogueBox;

    public Dialogue dialogue;
    [SerializeField]


    public void TriggerDialogue()
    {

        var dialogueManager = Instantiate(dialogueBox, new Vector3(0, 4.5f, 0), Quaternion.identity).GetComponentInChildren<DialogueManager>();
        
        dialogueManager.StartDialogue(dialogue);
    }

    

    private void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.X) && collision.gameObject.CompareTag("Player") && FindObjectOfType<DialogueManager>() == null)
        {
            //keydown = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().speed = 0;
            TriggerDialogue();
            
            if (gameObject.GetComponentInParent<PrincessQuestController>() != null)
            {
                PrincessQuestController questOne = gameObject.GetComponentInParent<PrincessQuestController>();
                questOne.dialogueInstantiated = true;
            }
            else
            {
                Debug.Log("Not Princess quest");
            }
        }
    }

    

}
