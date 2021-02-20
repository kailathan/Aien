using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public bool levelChanger;
    public bool NextLevel;
    public bool PrevLevel;
    private int level;
    public LevelLoader loader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (levelChanger == true)
            {
                if (NextLevel == true)
                {
                    loader.LoadNextLevel();
                }
                if (PrevLevel == true)
                {
                    loader.LoadPrevLevel();
                }
            }
            else if (levelChanger == false)
            {
                level = SaveManager.instance.activeSave.level;
                Vector3 pos = collision.gameObject.transform.position;
                SaveManager.instance.activeSave.spawnLocation[level] = pos.ToString();
                SaveManager.instance.Save();
                }
            }

        }
    }
