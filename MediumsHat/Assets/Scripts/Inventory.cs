using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> content = new List<Item>();

    //public static Inventory instance;

    public void mergeItems() {
        Item currentItem1 = content[0];
        Item currentItem2 = content[1];

        // Item créé (si les deux ont le même champ item result)
        // on add le nouvel item a content et on supprime les deux précedents)
        //content.add()
    }
}
