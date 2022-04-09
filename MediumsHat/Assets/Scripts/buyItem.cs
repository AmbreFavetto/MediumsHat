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

    public void BuyItem() {
        if(Inventory.instance.coinsCount >= cost) {
            Inventory.instance.content.Add(item);
            //interactUI.enabled = false;
            Destroy(gameObject);
            Inventory.instance.coinsCount -= cost;
            Inventory.instance.coinsCountText.text = Inventory.instance.coinsCount.ToString();
        }
        
    }
    
}
