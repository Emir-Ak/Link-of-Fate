﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class DialogueManager : MonoBehaviour {

    public Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    bool first = true;

    public Animator anim;
    public AnimationClip closeAnim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponentInParent<Animator>();
	}


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            DisplayNextSentence();
        }

            
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences = new Queue<string>();
        
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();

 
    }
  
    

    private void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();



        StopAllCoroutines();
        if (first)
        {
            StartCoroutine(TypeSentence(sentence, 0.05f));

        }
        else
        {
            StartCoroutine(TypeSentence(sentence, 0.03f));
        }


        
    }


    IEnumerator TypeSentence(string sentence,float rate)
    {


        first = false;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(rate);
        }

    }

    private void EndDialogue()
    {
        first = true;
        PlayerController player = FindObjectOfType<PlayerController>();

        player.speed = player.standardMoveSpeed;
        anim.SetBool("isOpen", false);
        Destroy(gameObject,closeAnim.length - .2f);
        
    }
}

