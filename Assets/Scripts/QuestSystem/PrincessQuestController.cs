using UnityEngine.UI;
using UnityEngine;

public class PrincessQuestController : MonoBehaviour {
    public int action = 0;
    public bool dialogueInstantiated = false;
    DialogueManager dialogueManager;
    bool finished = false;
    private void Update()
    {
        if (dialogueInstantiated == true)
        {
            dialogueManager = FindObjectOfType<DialogueManager>(); //srsly??? :<
            dialogueInstantiated = false;
        }

        if (dialogueManager != null && dialogueManager.sentences.Count == 0)
        {
            finished = true;
        }
        if(finished == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                action = 1;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                action = 2;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                action = 3;
            }
        }
    }
}
