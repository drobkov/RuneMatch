using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;

    public Animator animator;

    private Queue<string> sentencens;
    void Start()
    {
       sentencens = new Queue<string>(); 
    }

    public void StartDialogue(Dialogue dialogue){
        

        animator.SetBool("IsOpen", true);
        
        nameText.text = dialogue.name;

        sentencens.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentencens.Enqueue(sentence);
        }
        DisplayNextSentence();
        
    }
    
    public void DisplayNextSentence(){
        if (sentencens.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentencens.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Набор текта по букве 
    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    void EndDialogue(){
        animator.SetBool("IsOpen", false);
    }
}
