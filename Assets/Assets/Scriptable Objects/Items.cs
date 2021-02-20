using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ItemScriptableObject")]
public class Items : ScriptableObject
{

    public Item.ItemType itemType;
    public string itemName;
    public Sprite itemSprite;
    public bool isStackable;
}
