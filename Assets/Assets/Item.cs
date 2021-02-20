using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Gun,
        Sword,
        HealthPot,
        Bow
    }

    public ItemType itemType;

    public int amount;

   /* public Sprite GetSprite()
    {
        return items.itemSprite;
    }
   */
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.Bow: return ItemAssets.Instance.bowSprite;
            case ItemType.HealthPot: return ItemAssets.Instance.potSprite;
            case ItemType.Gun: return ItemAssets.Instance.gunSprite;
        }
    }
    /*public bool isItemStackable()
    {
        return items.isStackable;
    }*/
    public bool isItemStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return false;
            case ItemType.Bow: return false;
            case ItemType.HealthPot: return true;
            case ItemType.Gun: return false;

        }
    }
   
}
