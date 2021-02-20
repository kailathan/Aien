using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Default object", menuName = "Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}
