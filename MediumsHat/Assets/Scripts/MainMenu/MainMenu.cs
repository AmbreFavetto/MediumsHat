using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

using System;

using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // XML Loader
    XDocument xmlDoc;
    IEnumerable<XElement> choices;
    static List<XMLData> data;
    int choice = 0;
    int iteration = 0;
    int pageNum = 0;
    int pnjId = 0;
    string charText;
    string dialogueText;
    List<(int,string)> response;

    bool finishedLoading = false;

    // Menu
    public string levelToLoad;
    public GameObject settingsWindow;
    public Button btnStart;

    public String xmlDocumentPath;

    void Awake() {
        response = new List<(int,string)>();
        data = new List<XMLData>();
    }

     void Start() {
         btnStart.interactable = true;  
        LoadXML();
        StartCoroutine("AssignData");
    }

    void Update() {
        if(finishedLoading) {
            btnStart.interactable = true;
        }
    }

    public void StartGame() {
        Time.timeScale = 1;                 
        SceneManager.LoadScene(levelToLoad);   
        finishedLoading=false;
    }

    public void SettingsButton() {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow() {
        settingsWindow.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    void LoadXML() {
        xmlDoc = XDocument.Load(xmlDocumentPath);
        choices = xmlDoc.Descendants("page").Elements();
    }

    IEnumerator AssignData() {
        foreach(var eltsChoice in choices) {
            pnjId = int.Parse(eltsChoice.Parent.Parent.Attribute("id").Value);
            pageNum = int.Parse(eltsChoice.Parent.Attribute("number").Value);
            choice = int.Parse(eltsChoice.Attribute("branche").Value);

            charText = eltsChoice.Element("name").Value.Trim();
            dialogueText = eltsChoice.Element("dialogue").Value.Trim();
            
            response = new List<(int,string)>();

            XElement elt = eltsChoice.Element("responses");
            IEnumerable<XElement> tests = null;

            if(elt != null) {
                tests = elt.Elements();        
                foreach(var test in tests) {
                    response.Add((int.Parse(test.Attribute("choice").Value), test.Element("text").Value.Trim()));                   
                }     
            } else {
                response.Add((0,"NULL"));
            }    

            data.Add(new XMLData(response, choice, pnjId, pageNum, charText, dialogueText));
        }      
        foreach(var elt in data) {
            Debug.Log("repsonse : " + response + "choice : " + choice + "pnj : " + pnjId);
        }
        finishedLoading = true;
        yield return null;
    }

    public List<XMLData> getData() {
        return data;
    }
}


public class XMLData {
    public List<(int, string)> responsesSentences;
    public int choiceNum;
    public int pnjNum;
    public int pageNum;
    public string charText;
    public string dialogueText;

    public XMLData(List<(int, string)> responses, int choice, int pnj, int page, string character, string dialogue) {
        responsesSentences = responses;
        choiceNum = choice;
        pnjNum = pnj;
        pageNum = page;
        charText = character;
        dialogueText = dialogue;
    }
}

    
