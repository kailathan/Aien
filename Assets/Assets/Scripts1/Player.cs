using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] private UI_SkillTree skillTree;
    //[SerializeField] private UI_Inventory ui_inventory;
    [SerializeField] private weaponry weaponry;
    private PlayerSkills playerSkills;
    private double playerDamage = 20;
    private bool dualWield = false;
    private double playerAccuracy = 0.5;

    private Animator animator;

    private Inventory inventory;

    private Item currentItem;

    private int index;

    //for picking up item once
    private bool pickedUp = false;

    public bool CanUseDmgIncrease()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.DamageIncrease);
    }
    public bool CanUseAccuracyIncrease()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.IncreasedAccuracy);
    }
    public bool CanUseDualWield()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.DualWieldItems);
    }
    private void Awake()
    {
        playerSkills = new PlayerSkills();
        LoadPlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        //inventory = new Inventory(UseItem);
        animator = GetComponent<Animator>();
    }

    //SOME OLD INVENTORY STUFF
    /*
    private void Start()
    {
        skillTree.SetPlayerSkills(playerSkills);
        ui_inventory.SetInventory(inventory);

        inventory.OnItemRemoved += Inventory_OnItemRemoved;

        ItemWorld.SpawnItemWorld(new Vector2(5, 0), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector2(8, 0), new Item { itemType = Item.ItemType.HealthPot, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector2(6, 0), new Item { itemType = Item.ItemType.Bow, amount = 1 });
        currentItem = inventory.GetItemList()[0];
    }

    private void Inventory_OnItemRemoved(object sender, EventArgs e)
    {
        if (index < inventory.GetItemList().Count)
        {
            currentItem = inventory.GetItemList()[index];
            ui_inventory.showSelectedItem(index);
        }
        else if (index == inventory.GetItemList().Count && index != 0)
        {
            currentItem = inventory.GetItemList()[index - 1];
            ui_inventory.showSelectedItem(index - 1);
            index--;

        }
        else
        {
            print("inventory is empty");
            currentItem = null;
        } 
    }

    private void Update()
    {
        //dropping items
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentItem != null)
            {
                Item duplicateItem = new Item { itemType = currentItem.itemType, amount = currentItem.amount };

                inventory.RemoveItem(currentItem);
                float playerDir = transform.forward.z;
                if (playerDir > 0) ItemWorld.DropItem(new Vector2(transform.position.x + 0.8f, transform.position.y), duplicateItem);
                if (playerDir < 0) ItemWorld.DropItem(new Vector2(transform.position.x - 0.8f, transform.position.y), duplicateItem);

            }

        }
        //using items
        if (Input.GetMouseButtonDown(0))
        {
            UseItem(currentItem);
        }
        //shifting item position in inventory
        for (int i = 0; i <= inventory.GetItemList().Count; i++) { 
        if (Input.GetKeyDown("" + i))
        {
            currentItem = inventory.GetItemList()[i-1];
            ui_inventory.showSelectedItem(i-1);
            index = inventory.GetItemList().IndexOf(currentItem);
            }
        }
    }

    public Item getCurrentItem()
    {
        return currentItem;
    }

    //picking up item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            Destroy(collision.gameObject);
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sword:
                StartCoroutine(weaponry.stab());
                break;
            case Item.ItemType.Bow:
                StartCoroutine(weaponry.shoot2());
                break;
            case Item.ItemType.HealthPot:
                print("Healed!");
                animator.SetTrigger("usePot");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPot, amount = 1 });
                Invoke("consumePot", 0.5f);
                break;
        }
    }
    */
    private void consumePot()
    {
        animator.ResetTrigger("usePot");
    }
    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case PlayerSkills.SkillType.DamageIncrease:
               playerDamage = playerDamage  + 0.1 * playerDamage;
                break;
            case PlayerSkills.SkillType.DualWieldItems:
                dualWield = true;
                break;
            case PlayerSkills.SkillType.IncreasedAccuracy:
                playerAccuracy = playerAccuracy + 0.1;
                break;
        }
    }
    //strictly for button use
    public void UnlockDamageIncrease()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.SkillType.DamageIncrease);
        SaveManager.instance.activeSave.playerDamage = playerDamage;
        SaveManager.instance.Save();
        //for the save system
        SavePlayer();
    }
    public void UnlockAccuracyIncrease()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.SkillType.IncreasedAccuracy);
        SaveManager.instance.activeSave.playerAccuracy = playerAccuracy;
        SaveManager.instance.Save();
        SavePlayer();
    }
    public void UnlockDualWield()
    {
        playerSkills.TryUnlockSkill(PlayerSkills.SkillType.DualWieldItems);
        SaveManager.instance.activeSave.canUseDualWield = dualWield;
        SaveManager.instance.Save();
        SavePlayer();
    }

    //concerning the save stuff
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayerSkills()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {

            if (data.unlockedAttack1) playerSkills.unlockedSkillTypeList.Add(PlayerSkills.SkillType.DamageIncrease);

            if (data.unlockedAttack2) playerSkills.unlockedSkillTypeList.Add(PlayerSkills.SkillType.IncreasedAccuracy);

            if (data.unlockedAttack3) playerSkills.unlockedSkillTypeList.Add(PlayerSkills.SkillType.DualWieldItems);
        }

    }
    public void DeletePlayerSave() {
        SaveSystem.DeleteSaveFile();
}
}
