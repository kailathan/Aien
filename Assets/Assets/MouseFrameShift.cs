using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseFrameShift : MonoBehaviour
{
    /*World system: Mora => 1, Dor => 2, Aeon => 3
     */
    public CinemachineVirtualCamera vcam;
    public float offsetY; 

    // Start is called before the first frame update
    void Start()
    {
        SaveManager.instance.Load();

    }

    // Update is called once per frame
    void Update()
    {
        //if trying to scroll to Mora
        if (vcam.m_Follow == GameObject.Find("Dór").transform && Input.mousePosition.x == 0)
        {
            vcam.m_Follow = GameObject.Find("Mora").transform;
        }
        //if trying to scroll to Dor
        if (vcam.m_Follow == GameObject.Find("Mora").transform && Input.mousePosition.x >= Screen.width - 1)
        {
            if (SaveManager.instance.activeSave.worldsUnlocked >= 2)
            {
                vcam.m_Follow = GameObject.Find("Dór").transform;
            }
            else
            {
                print("You havent unlocked that world yet!");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SaveManager.instance.activeSave.worldsUnlocked = 3;
            print("Added world 3");
            SaveManager.instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SaveManager.instance.activeSave.worldsUnlocked = 2;
            print("Added world 2");
            SaveManager.instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveManager.instance.activeSave.worldsUnlocked = 1;
            print("Removed world 2");
            SaveManager.instance.Save();
        }
    }

    }

