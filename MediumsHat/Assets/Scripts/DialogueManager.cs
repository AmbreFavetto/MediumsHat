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
    private Queue<string> names;
    private List<KeyValuePair<int, string>> responses;

    public static DialogueManager instance;
    
    public Button prefabResponse;
    public GameObject dialogueChoicesUI;
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
        names = new Queue<string>();
        responses = new List<KeyValuePair<int, string>>();

    }
    public void startDialogue(int pnj)
    {
        Time.timeScale = 0;
        dialogueUI.SetActive(true);

        names.Clear();
        sentences.Clear();
        responses.Clear();

        Loader loader = new Loader();
        List<XMLData> data=loader.getData();
       
        foreach(var elt in data) {
            if(elt.pnjNum == pnj) {
                //
                foreach(var test in elt.responsesSentences) {
                        Debug.Log(test);
                }
                //
                names.Enqueue(elt.charText);
                sentences.Enqueue(elt.dialogueText);
                responses = elt.responsesSentences;
// PB récuperation responses : récupère des NULL pour tout meme quand il y a des reponses
            }           
        }            
        DisplayNextSentense();
    }

    public void DisplayNextSentense(int choice = 0)
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        CharacterName.text = names.Dequeue();
        dialogueText.text = sentences.Dequeue();
                
        foreach(var responseSentence in responses) {
            Debug.Log(responseSentence.Value);
            if(responseSentence.Value != "NULL") { 
                Button newResponse = Instantiate(prefabResponse);
                newResponse.transform.SetParent(dialogueChoicesUI.transform, false);
                newResponse.GetComponentInChildren<Text>().text = responseSentence.Value;
            }                     
        }
        
    }
    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("fin du dialogue");

    }

    public void setChoice(int choiceNum) {

    }

}
 