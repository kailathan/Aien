using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {

            if (SceneManager.GetActiveScene().buildIndex +1 == 6)
            {
                print("Yay you won");
            }
            //MOVE TO   NEXT LEVEL
            SceneManager.LoadScene(nextSceneLoad);

            //setting int for index
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
    //for buttons
    public void moveToLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void resetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
