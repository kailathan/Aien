using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;

    public void LoadNextLevel()
    {
        StartCoroutine(Loadlevel(SceneManager.GetActiveScene().buildIndex + 1)); 
    }
    public void LoadPrevLevel()
    {
        StartCoroutine(Loadlevel(SceneManager.GetActiveScene().buildIndex - 1));

    }
    public int getLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    IEnumerator Loadlevel(int levelIndex)
    {
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(levelIndex);

        SaveManager.instance.activeSave.level = levelIndex;

        SaveManager.instance.Save();

        if(levelIndex > 0)
        {
            anim.SetTrigger("Start");
        }
    }
}
