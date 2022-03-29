using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> content = new List<Item>();

    // Menu
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject itemInventoryUI;

    public GameObject prefabInventorySlot;

    public static bool inventoryIsOpen = false;
    //public int contentCurrentIndex = 0;
    public static Inventory instance;

    public int coinsCount;
    public Text coinsCountText;

    private void Awake() {
        if(instance != null) 
        {
            Debug.LogWarning("Il y a plus d'une instance d'Inventory dans la scène");
            return;
        }
        instance = this;

        coinsCountText.text = coinsCount.ToString();
    }

    void Update() {
        if(Input.GetButtonDown("Inventory")) {
            if(inventoryIsOpen) {
                CloseInventoryUI();
            } else {
                OpenInventoryUI();
            }
        }
    }

    public void UpdateInventoryUI() {
        foreach(Item contentItem in content) {
            GameObject newItem = Instantiate(prefabInventorySlot);
            newItem.transform.SetParent(itemInventoryUI.transform, false);
            var newItemInfos = newItem.GetComponent<Image>();
            newItemInfos.sprite = contentItem.image;           
        }
    }

    public void OpenInventoryUI() {
        if(inventoryUI.activeSelf == false) {
            UpdateInventoryUI();
            inventoryIsOpen = true;
            Time.timeScale = 0;
        }       
        inventoryUI.SetActive(true);       
    }

    public void CloseInventoryUI() {
        if(pauseMenuUI.activeSelf == false) {
            Time.timeScale = 1;
        }
        inventoryIsOpen = false;
        inventoryUI.SetActive(false);
        foreach(Transform child in itemInventoryUI.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void mergeItems() {
        Item currentItem1 = content[0];
        Item currentItem2 = content[1];

        // Item créé (si les deux ont le même champ item result)
        // on add le nouvel item a content et on supprime les deux précedents)
        //content.add()
    }

    public void useCoins(int cost) {
        coinsCount -= cost;
        coinsCountText.text = coinsCount.ToString();
    }

    // public void addItemToInventory(Item item) {
    //     print("lezgooo ");
    //     content.Add(item);
    // }
}
