using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

public class Loader : MonoBehaviour
{
    XDocument xmlDoc;
    IEnumerable<XElement> choices;
    static List<XMLData> data;
    int choice = 0;
    int iteration = 0;
    int pageNum = 0;
    int pnjId = 0;
    string charText;
    string dialogueText;
    List<KeyValuePair<int, string>> response;

    bool finishedLoading = false;

    void Awake() {
        response = new List<KeyValuePair<int, string>>();
        data = new List<XMLData>();
    }
    void Start() {
        DontDestroyOnLoad(gameObject);
        LoadXML();
        StartCoroutine("AssignData");
    }

    void Update() {
        if(finishedLoading) {
            Application.LoadLevel("MainMenu");
            finishedLoading=false;
        }
    }

    void LoadXML() {
        xmlDoc = XDocument.Load("Assets/dialogues.xml");
        choices = xmlDoc.Descendants("page").Elements();
    }

    IEnumerator AssignData() {
        var i = 0;
        foreach(var eltsChoice in choices) {
            pnjId = int.Parse(eltsChoice.Parent.Parent.Attribute("id").Value);
            pageNum = int.Parse(eltsChoice.Parent.Attribute("number").Value);
            choice = int.Parse(eltsChoice.Attribute("branche").Value);
            charText = eltsChoice.Element("name").Value.Trim();
            dialogueText = eltsChoice.Element("dialogue").Value.Trim();

            if(response != null) {
                response.Clear();
            }

            XElement elt = eltsChoice.Element("responses");
            IEnumerable<XElement> tests = null;

            if(elt != null) {
                tests = elt.Elements();        
                foreach(var test in tests) {
                    response.Add(new KeyValuePair<int, string>(int.Parse(test.Attribute("choice").Value) ,test.Element("text").Value.Trim()));                   
                }     
            } else {
                response.Add(new KeyValuePair<int, string>(0, "NULL"));
            }    

            Debug.Log(i);
            data.Add(new XMLData(response, choice, pnjId, pageNum, charText, dialogueText));
            foreach(var xmldata in data) {
                foreach(var reponse in xmldata.responsesSentences) {
                    Debug.Log(reponse);
                }
            }
            
            i++;
        }      

        finishedLoading = true;
        yield return null;
    }

    public List<XMLData> getData() {
        return data;
    }
}


public class XMLData {
    public List<KeyValuePair<int, string>> responsesSentences;
    public int choiceNum;
    public int pnjNum;
    public int pageNum;
    public string charText;
    public string dialogueText;

    public XMLData(List<KeyValuePair<int, string>> responses, int choice, int pnj, int page, string character, string dialogue) {
        responsesSentences = responses;
        choiceNum = choice;
        pnjNum = pnj;
        pageNum = page;
        charText = character;
        dialogueText = dialogue;
    }
}
