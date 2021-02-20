using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlTypes;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour

{
    public SaveData activeSave;

    public static SaveManager instance;

    public bool hasLoaded;

    public string savePath;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            Load();
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DeleteSaveFile();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
    }

    public void Save()
    {
        //using binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string dataPath = Application.persistentDataPath;
        //var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        //serializer.Serialize(stream, activeSave);
        formatter.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("Saved");
    }
    //for saving player inventory
    public void SaveInventory(Inventory Container)
    {
        /*
        string saveData = JsonUtility.ToJson(inventory, true);
        //using binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        string dataPath = Application.persistentDataPath;
        FileStream file = File.Create(dataPath + savePath);
        formatter.Serialize(file, saveData);
        file.Close();
        Debug.Log("Saved");
        */
        string dataPath = Application.persistentDataPath;
        IFormatter iFormatter = new BinaryFormatter();
        Stream stream = new FileStream(dataPath + savePath, FileMode.Create, FileAccess.Write);
        iFormatter.Serialize(stream, Container);
    }
    
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        Debug.Log(dataPath);
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            //var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            //activeSave = serializer.Deserialize(stream) as SaveData;
            activeSave = formatter.Deserialize(stream)as SaveData;
            stream.Close();
            hasLoaded = true;
        }
    }
    //for loading player inventory
    public void LoadInventory(Inventory Container)
    {
        if(File.Exists(Application.persistentDataPath + savePath))
        {
            /*
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open((Application.persistentDataPath + savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(file).ToString(), inventory);
            file.Close();
            */
            string dataPath = Application.persistentDataPath;
            IFormatter iFormatter = new BinaryFormatter();
            Stream stream = new FileStream(dataPath + savePath, FileMode.Open, FileAccess.Read);
            Container = (Inventory)iFormatter.Deserialize(stream);
            stream.Close();

        }
    }

    public void DeleteSaveFile()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
            Debug.Log("Deleted!");
        }
        else
        {
            Debug.Log("Nothing to delete!");
        }
    }
}

[System.Serializable]
public class SaveData{

    public string saveName;

    public string[] spawnLocation;

    public int balance;

    public int exp;

    public int levelExp;

    public int level;

    public double playerDamage;

    public double playerAccuracy;

    public bool canUseDualWield;

    public int worldsUnlocked;

    }

[System.Serializable]
public class PlayerData
{
    public int dps;

    public int health;

    public string[] inventory;

    public bool unlockedAttack1;

    public bool unlockedAttack2;

    public bool unlockedAttack3;

    public List<int> worldsUnlocked;

    public int currentWorld;


    public PlayerData(Player player)
    {
        unlockedAttack1 = player.CanUseDmgIncrease();
        unlockedAttack2 = player.CanUseAccuracyIncrease();
        unlockedAttack3 = player.CanUseDualWield();
    }
}
//for saving player stuff, uses binary formatter
public static class SaveSystem
{
    
    public static void SavePlayer(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("SaveSystem saved");
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Gone wrong");
            return null;
        }
    }
    public static void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save System file deleted!");
        }
        else
        {
            Debug.Log("Nothing to delete!");
        }
    }
}

