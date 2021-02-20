using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    private ItemDatabaseObject database;

    public Image itemSelector;
    private PlayerInventory playerInventory;
    public InventoryObject inventory;
    //space between items
    public int xBwItems;
    public int yBwItems;
    //number of columns
    public int numColumns;
    Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
 /*   void Start()
    {
        CreateDisplay();
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector2.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public Vector2 getPosition(int i)
    {

        return new Vector2(xBwItems*(i % numColumns), (-yBwItems * (i/numColumns)));
    }

    // Update is called once per frame
    void Update()
    {
        updateDisplay();
    }
    public void updateDisplay(){
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemDisplayed[inventory.Container[i]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector2.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =; 
                obj.GetComponent<RectTransform>().localPosition = getPosition(i);
                obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemDisplayed.Add(inventory.Container[i], obj);
            }
        }
        itemSelector.GetComponent<RectTransform>().localPosition = getPosition(playerInventory.currentItemIndex);
    }
 */

}
