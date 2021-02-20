using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;

    private Vector3 pos;
    private GameObject p;
    private GameObject[] Camera;
    public Transform defaultspawn;
    private int level;
    public LevelLoader loader;
    private LevelSystem levelSystem;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //exp stuff set up first
        levelSystem = new LevelSystem();
        levelSystem.setExperience(SaveManager.instance.activeSave.exp);
        levelSystem.SetLevelNumber(SaveManager.instance.activeSave.levelExp);
        levelWindow.SetLevelSystem(levelSystem);
        animator = GameObject.Find("UI_SkillTree").GetComponent<Animator>();

        //set up the world
        level = loader.getLevel();
        SaveManager.instance.activeSave.level = level;
        if (SaveManager.instance.hasLoaded && SaveManager.instance.activeSave.spawnLocation[level].Length != 0)
        {
            pos = StringToVector3(SaveManager.instance.activeSave.spawnLocation[level]);
            p = GameObject.FindGameObjectWithTag("Player");
            p.transform.position = pos;
            Camera = GameObject.FindGameObjectsWithTag("House");
            for (int i = 0; i < Camera.Length; i++)
                {
                    Camera[i].SetActive(false);
                }
        }else{

            //if the spawn point doesn't exist, spawn the player in the default spawn
            p = GameObject.FindGameObjectWithTag("Player");
            p.transform.position = defaultspawn.position;
            print("spawning in default spawn");
        }
    }
    
    //For turning vector3.toString() back into Vector3
    public Vector3 StringToVector3(string s)
    {
        string[] temp = s.Substring(1, s.Length - 2).Split(',');
        return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            levelSystem.AddExperience(15);
            SaveManager.instance.activeSave.exp = levelSystem.getExperience();
            SaveManager.instance.activeSave.levelExp = levelSystem.GetLevelNumber();
            SaveManager.instance.Save();
            print("Now you have " + levelSystem.getExperienceNormalized() + " exp");
            print("You are now at level: " + levelSystem.GetLevelNumber());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenSkillTree();
            Invoke("CloseSkillTree", 5f);
        }
    }

    public void OpenSkillTree()
    {
        animator.SetBool("SkillTreeOpen", true);
    }
    public void CloseSkillTree()
    {
        animator.SetBool("SkillTreeOpen", false);
    }
}
