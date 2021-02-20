using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public Inventory Container;
    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        Container.Items.Add(new InventorySlot(database.getID[_item], _item, _amount));
    }
    [ContextMenu("Save")]
    public void Save()
    {
        SaveManager.instance.SaveInventory(Container);
    }
    [ContextMenu("Load")]

    public void Load()
    {
        SaveManager.instance.LoadInventory(Container);
    }
    [ContextMenu("Clear")]

    public void Clear()
    {
        Container = new Inventory();
    }
}
[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();

}
[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
