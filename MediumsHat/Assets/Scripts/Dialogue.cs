using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class Dialogue
{
    public string[] charactersName;
    
    public int[] peopleTalkingId;
    [TextArea(3, 10)]
    public string[] peopleTalkingText;
}
 