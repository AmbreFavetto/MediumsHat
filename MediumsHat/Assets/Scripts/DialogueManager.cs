using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
     public string[] charactersName;
    public Text dialogueText;
    public Text CharacterName;
    private Queue<string> sentences;

    public static DialogueManager instance;
    public GameObject dialogueUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scene");
            return;
        }
        instance = this;

        sentences = new Queue<string>();

    }
    public void startDialogue(int pnj)
    {
        Time.timeScale = 0;
        dialogueUI.SetActive(true);
        sentences.Clear();

        Loader loader = new Loader();
        List<XMLData> data=loader.getData();

        foreach(var elt in data) {
            if(elt.pnjNum == pnj) {
                Debug.Log(elt.pnjNum);
                //charactersName.Add(elt.charText);
                sentences.Enqueue(elt.dialogueText);
            }           
        }            
        DisplayNextSentense();
    }

    public void DisplayNextSentense()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = sentences.Dequeue();


    }
    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("fin du dialogue");

    }

}
 