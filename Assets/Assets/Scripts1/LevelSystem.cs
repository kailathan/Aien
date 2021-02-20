using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem
{

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    private int level;
    private int experience;
    private int experienceToNextLevel;
    private int initExp = 0;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }
    public void AddExperience(int amount)
    {
        
        initExp = experience;
        experience = experience + amount;
        if(experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }
    public int GetLevelNumber()
    {
        return level;
    }
    public void SetLevelNumber(int number)
    {
        level = number;
    }
    public float getExperienceNormalized()
    {
        return (float) experience / experienceToNextLevel;
    }
    public void setExperience(int number)
    {
        experience = number; 
    }
    public int getExperience()
    {
        return experience;
    }
    public float GetInitExpNormalized()
    {
        return (float)initExp / experienceToNextLevel;
    } 

}