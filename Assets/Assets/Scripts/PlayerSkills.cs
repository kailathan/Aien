using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerSkills 
{
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }
    public enum SkillType
    {
        //Attack
        None,
        DamageIncrease,
        IncreasedAccuracy,
        DualWieldItems
    }

    public List<SkillType> unlockedSkillTypeList;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }
    private void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType)) { 
            unlockedSkillTypeList.Add(skillType);  
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
        else
        {
            Debug.Log("this skill has already been unlocked");
        }
    }
    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }
    public bool CanUnlock(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);
        if(skillRequirement != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.IncreasedAccuracy: return SkillType.DamageIncrease;
            case SkillType.DualWieldItems: return SkillType.IncreasedAccuracy;
        }
        return SkillType.None;
    }
    public bool TryUnlockSkill(SkillType skillType)
    {
        if (CanUnlock(skillType))
        {
            UnlockSkill(skillType);
            Debug.Log("You've unlocked this skill type");
            return true;
        }
        else
        {
            Debug.Log("You cannot unlock this one!");
            return false;
        }


    }
}
