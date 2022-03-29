using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyItem : MonoBehaviour
{
    //private Text interactUI;
    private bool isInRange;

    public Item item;
    public int cost;
    //public AudioClip soundToPlay;

    //void Awake() {
        //interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    //}

    void Update() {
        if (Input.GetMouseButtonDown(0) && isInRange) {           
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                BuyItem();
            }
        }
    }

    void BuyItem() {
        if(Inventory.instance.coinsCount >= cost) {
            Inventory.instance.content.Add(item);
            //interactUI.enabled = false;
            Destroy(gameObject);
            Inventory.instance.coinsCount -= cost;
            Inventory.instance.coinsCountText.text = Inventory.instance.coinsCount.ToString();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            //interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            //interactUI.enabled = false;
            isInRange = false;
        }
    }
    
}
