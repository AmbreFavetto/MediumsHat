using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public GameObject player;
    float highestPoint;
    float leftPoint;
    float rightPoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<PolygonCollider2D>().enabled = false;
        highestPoint = transform.GetComponent<PolygonCollider2D>().points[0].y;
        leftPoint = rightPoint = transform.GetComponent<PolygonCollider2D>().points[0].x;
        for (int i = 1; i < transform.GetComponent<PolygonCollider2D>().points.Length; i++){
            if (transform.GetComponent<PolygonCollider2D>().points[i].y > highestPoint) highestPoint = transform.GetComponent<PolygonCollider2D>().points[i].y;
            if (transform.GetComponent<PolygonCollider2D>().points[i].x < leftPoint) leftPoint = transform.GetComponent<PolygonCollider2D>().points[i].x;
            if (transform.GetComponent<PolygonCollider2D>().points[i].x > rightPoint) rightPoint = transform.GetComponent<PolygonCollider2D>().points[i].x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > highestPoint) {
            if (player.transform.position.x < leftPoint || player.transform.position.x > rightPoint)
            {
                transform.GetComponent<PolygonCollider2D>().enabled = true;
                player.GetComponent<PlayerController>().stairsEnable = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (player.transform.position.x < leftPoint || player.transform.position.x > rightPoint)
            {
                transform.GetComponent<PolygonCollider2D>().enabled = true;
                player.GetComponent<PlayerController>().stairsEnable = true;
            }
        }
        if (!player.GetComponent<PlayerController>().stairsEnable)
        {
            transform.GetComponent<PolygonCollider2D>().enabled = false;
        }
        
    }
}
