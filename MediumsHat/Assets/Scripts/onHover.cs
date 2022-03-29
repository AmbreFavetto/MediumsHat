using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHover : MonoBehaviour
{
    public Shader Outline;
    public Shader Default;

    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().material.shader = Outline;
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().material.shader = Default;
    }
}
