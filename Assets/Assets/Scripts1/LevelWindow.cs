using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    private Image barImage;
    private Text levelText;
    private LevelSystem levelSystem;

    float lerpDuration = 1;
    float lerpValue;

    float currentExpSize;
    private float speed = 10;

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();
        levelText = transform.Find("text").GetComponent<Text>();
    }
    private void SetExpBarSize(float expNormalized)
    {
        //barImage.fillAmount = Mathf.Lerp(levelSystem.GetInitExpNormalized(), expNormalized, Time.deltaTime*speed);
        //barImage.fillAmount = Mathf.Lerp(0, 0.5f, Time.deltaTime*speed);
        StartCoroutine(LerpExp(levelSystem.GetInitExpNormalized(), expNormalized));

    }
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "" + levelNumber;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExpBarSize(levelSystem.getExperienceNormalized());

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.GetLevelNumber());
    }
    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExpBarSize(levelSystem.getExperienceNormalized());
 
    }
    IEnumerator LerpExp(float initexp, float finalexp)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            barImage.fillAmount = Mathf.Lerp(initexp, finalexp, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        lerpValue = finalexp;
    }
}
