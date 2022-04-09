using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;

public class DialogueManager : MonoBehaviour
{
    public string[] charactersName;
    public Text dialogueText;
    public Text CharacterName;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<int> choices;
    private Queue<List<(int,string)>> responses;
    List<GameObject> btnInstances;

    public static DialogueManager instance;
    
    public GameObject prefabResponse;
    public GameObject dialogueChoicesUI;
    public GameObject objectToBuyUI;
    public GameObject dialogueUI;
    public GameObject btnNextSentenceUI;
    public List<GameObject> prefabObjToBuy;
    public Item item;

    GameObject newObject = null;

    bool isMerchante = false;

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
        choices = new Queue<int>();
        responses = new Queue<List<(int, string)>>();
        btnInstances = new List<GameObject>();

    }
    public void startDialogue(int pnj)
    {
        Time.timeScale = 0;
        dialogueUI.SetActive(true);

        sentences.Clear();
        names.Clear();
        choices.Clear();
        responses.Clear();

        Loader loader = new Loader();
        List<XMLData> data=loader.getData();

        foreach(var elt in data) {
            if(elt.pnjNum == pnj) {
                // merchante
                if(elt.pnjNum == 2) {
                    isMerchante = true;
                } else {
                    isMerchante = false;
                }
                names.Enqueue(elt.charText);
                sentences.Enqueue(elt.dialogueText);
                choices.Enqueue(elt.choiceNum);
                List<(int, string)> tempResponses = new List<(int, string)>();
                foreach(var responseSentence in elt.responsesSentences) {
                    tempResponses.Add(responseSentence);
                }
                responses.Enqueue(tempResponses);
            }          
        }           
        DisplayNextSentence();
    }

    public void DisplayNextSentence(int choice = 0)
    {
        if(newObject != null) {
            Destroy(newObject.gameObject);
        }

        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if(choices.Dequeue() == choice) {
            CharacterName.text = names.Dequeue();
            dialogueText.text = sentences.Dequeue(); 
            List<(int choice, string response)> tempResponses = new List<(int choice, string response)>();
            tempResponses = responses.Dequeue();               
            foreach(var response in tempResponses) {
                if(isMerchante) {
                    foreach(var obj in prefabObjToBuy) {
                        newObject = Instantiate(obj);
                        newObject.transform.SetParent(objectToBuyUI.transform, false);
                        newObject.GetComponentInChildren<Button>().onClick.AddListener(delegate(){BuyItem(15, item);});
                    }                    
                }
                if(response.response != "NULL") { 
                    btnNextSentenceUI.SetActive(false);
                    GameObject newResponse = Instantiate(prefabResponse);
                    btnInstances.Add(newResponse);
                    newResponse.transform.SetParent(dialogueChoicesUI.transform, false);
                    newResponse.GetComponentInChildren<Text>().text = response.response; 
                    newResponse.GetComponentInChildren<Button>().onClick.AddListener(delegate(){DisplayNextSentenceWithChoice(response.choice);});           
                } else {
                    btnNextSentenceUI.SetActive(true);
                }  
            }
        } else {
            names.Dequeue();
            sentences.Dequeue();
            responses.Dequeue();
            DisplayNextSentence(choice);
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

    public void DisplayNextSentenceWithChoice(int choice) {
        Debug.Log("coucou");
        //Destroy(dialogueChoicesUI.GetComponentInChildren<Button>().gameObject);
        foreach(var btn in btnInstances) {
            Destroy(btn.gameObject);
        }
        DisplayNextSentence(choice);
    }

    public void BuyItem(int cost, Item item) {
        if(Inventory.instance.coinsCount >= cost) {
            Inventory.instance.content.Add(item);
            Inventory.instance.coinsCount -= cost;
            Inventory.instance.coinsCountText.text = Inventory.instance.coinsCount.ToString();
        }
        
    }

}
 