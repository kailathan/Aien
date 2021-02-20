using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockedMaterial;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;

    private void UpdateVisuals()
    {
        foreach(SkillButton skillButton in skillButtonList)
        {
            skillButton.UpdateVisuals();
        }
    }
    public void SetPlayerSkills(PlayerSkills playerSkills){
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(transform.Find("AccuracyIncreaseButton"), playerSkills, PlayerSkills.SkillType.IncreasedAccuracy, skillUnlockedMaterial, skillLockedMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("DualWieldButton"), playerSkills, PlayerSkills.SkillType.DualWieldItems, skillUnlockedMaterial, skillLockedMaterial));
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        UpdateVisuals();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        UpdateVisuals();
    }

    private class SkillButton
    {
        private Transform transform;
        private Image image;
        private Image bgImage;
        private PlayerSkills playerSkills;
        private PlayerSkills.SkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockedMaterial;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType, Material skillUnlockedMaterial, Material skillLockedMaterial)
        {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillUnlockedMaterial = skillUnlockedMaterial;
            this.skillLockedMaterial = skillLockedMaterial;

        }
        public void UpdateVisuals()
        {
            image = transform.GetComponent<Image>();
            //if the player has already unlocked the preceding skill
            if (playerSkills.IsSkillUnlocked(skillType))
            {
                image.material = null;
            }
            else
            {
                if (playerSkills.CanUnlock(skillType))
                {
                    image.material = skillUnlockedMaterial;
                }
                else
                {
                    image.material = skillLockedMaterial;
                }
            }
        }
    }
}
