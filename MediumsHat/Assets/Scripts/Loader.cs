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
    IEnumerable<XElement> pnjs;
    IEnumerable<XElement> items;
    static List<XMLData> data = new List<XMLData>();
    int iteration = 0;
    int pageNum = 0;
    int pnjId = 0;
    string charText;
    string dialogueText;
    bool finishedLoading = false;

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
        pnjs = xmlDoc.Descendants("pnj"); 

        //items = xmlDoc.Descendants("pages").Elements();
    }


    IEnumerator AssignData() {
        foreach(var pnj in pnjs) {
            items = xmlDoc.Descendants("page").Elements();
            foreach(var item in items) {
                if(item.Parent.Attribute("number").Value == iteration.ToString()) {
                    pnjId = int.Parse(item.Parent.Parent.Attribute("id").Value);
                    pageNum = int.Parse(item.Parent.Attribute("number").Value);
                    charText = item.Parent.Element("name").Value.Trim();
                    dialogueText = item.Parent.Element("dialogue").Value.Trim();
                    data.Add(new XMLData(pnjId, pageNum, charText, dialogueText));
                    iteration++;
                }
            }
            
        }
        finishedLoading = true;
        yield return null;
    }

    // IEnumerator AssignData() {
    //         iteration = 0;
    //         foreach(var item in items) {
    //             if(item.Parent.Attribute("number").Value == iteration.ToString()) {
    //                 pnjId = int.Parse(item.Parent.Parent. Attribute("id").Value);
    //                 Debug.Log("pnjId " + pnjId);
    //                 pageNum = int.Parse(item.Parent.Attribute("number").Value);
    //                 Debug.Log("pageNum "+ pageNum);
    //                 charText = item.Parent.Element("name").Value.Trim();
    //                 dialogueText = item.Parent.Element("dialogue").Value.Trim();
    //                 Debug.Log("dialogue text "+ dialogueText);
    //                 data.Add(new XMLData(pageNum, charText, dialogueText));
    //                 iteration++;
    //             }
    //         }
    //         finishedLoading = true;
    //         yield return null;
    // }
        
        
    
    public List<XMLData> getData() {
        return data;
    }
}



public class XMLData {
    public int pnjNum;
    public int pageNum;
    public string charText;
    public string dialogueText;

    public XMLData(int pnj, int page, string character, string dialogue) {
        pnjNum = pnj;
        pageNum = page;
        charText = character;
        dialogueText = dialogue;
    }
}
