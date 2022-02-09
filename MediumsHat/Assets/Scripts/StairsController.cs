using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && player.GetComponent<PlayerController>().stairsEnable)
        {
            transform.GetComponent<PolygonCollider2D>().enabled = true;
        }
        if (!player.GetComponent<PlayerController>().stairsEnable)
        {
            transform.GetComponent<PolygonCollider2D>().enabled = false;
        }
        
    }
}
