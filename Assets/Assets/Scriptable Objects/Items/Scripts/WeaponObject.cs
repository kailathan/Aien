using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Weapon Object", menuName = "Inventory System/Items/Weapon")]

public class WeaponObject : ItemObject
{
    public float atkBonus;
    public float range;
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
