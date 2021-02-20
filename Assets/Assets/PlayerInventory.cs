using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public InventoryObject inventory;
    public int currentItemIndex;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<PlayerItem>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(collision.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
  //      inventory.Container.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
        }
        //changing your current selected items through hotkey
        for (int i = 0; i < inventory.Container.Items.Count+1; i++)
        {
            if (Input.GetKeyDown(i + ""))
            {
                currentItemIndex = i-1;
                switch (inventory.Container.Items[i - 1].ID)
                {
                    case 0:
                        print("Tis a template item");
                        break;
                    case 1:
                        print("Tis a potion");
                        break;
                    case 2:
                        print("stabby");
                        break;
                    case 3:
                        print("pew pew");
                        break;
                }
            }
        }
    }






    //part of old tutorial
    public bool[] isFull;
    public GameObject[] slots;

}
public class PlayerItems: MonoBehaviour
{
    public enum ItemType
    {
        Sword,
        Potion,
        Armor
    }

    public ItemType itemType;
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.Potion: return ItemAssets.Instance.potSprite;
            case ItemType.Armor: return ItemAssets.Instance.armorSprite;
        }
    }
}
