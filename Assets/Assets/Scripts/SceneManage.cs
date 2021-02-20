using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    // Update is called once per frame
    public LevelLoader levelLoader;
    private Tooltip tooltip;
    public Material itemSelectedMaterial;
    private Transform t;

    public Vector2 ogSize;
    public Vector2 selectedSize;

    private GameObject Aeon;
    private GameObject Dor;

    private GameObject user;

    private int currentWorldsUnlocked;

    private void Start()
    {
        //ogSize = transform.Find("Aeon").GetComponent<RectTransform>().sizeDelta;
       // selectedSize = new Vector2(transform.Find("Aeon").GetComponent<RectTransform>().sizeDelta.x + 20, transform.Find("Aeon").GetComponent<RectTransform>().sizeDelta.y + 20);
        SaveManager.instance.Load();
        Aeon = GameObject.Find("Aeon");
        Dor = GameObject.Find("Dór");
        user = GameObject.Find("User");
        updateWorlds();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.name == "Mora" || (hit.collider.name == "Dór" && SaveManager.instance.activeSave.worldsUnlocked >=2))
            {
                revealPlanet(hit);
            }
        }
        else if (t != null)
        {
            tooltip.HideToolTip();
        }
        //loading in new worlds if they get unlocked
        if (SaveManager.instance.activeSave.worldsUnlocked != currentWorldsUnlocked)
        {
            updateWorlds();
        }
    }
    //to show planet details and launch rocket to that planet if the player has unlocked it
    public void revealPlanet(RaycastHit2D hit)
    {
        t = hit.collider.transform;
        tooltip = t.GetComponentInChildren<Tooltip>();
        tooltip.ShowToolTip();
        user.transform.position = new Vector2(t.position.x, t.position.y + 0.8f);
    }

    public void updateWorlds()
    {
        //unload all the worlds that arent unlocked
        if (SaveManager.instance.activeSave.worldsUnlocked == 1)
        {
            Aeon.transform.Find("Line").GetComponent<SpriteRenderer>().enabled = false;
            Aeon.GetComponent<SpriteRenderer>().color = Color.black;
            Dor.transform.Find("Line").GetComponent<SpriteRenderer>().enabled = false;
            Dor.GetComponent<SpriteRenderer>().color = Color.black;
            currentWorldsUnlocked = 1;
        }
        else if (SaveManager.instance.activeSave.worldsUnlocked == 2)
        {
            Aeon.transform.Find("Line").GetComponent<SpriteRenderer>().enabled = false;
            Aeon.GetComponent<SpriteRenderer>().color = Color.black;
            Dor.transform.Find("Line").GetComponent<SpriteRenderer>().enabled = true;
            Dor.GetComponent<SpriteRenderer>().color = Color.white;
            currentWorldsUnlocked = 2;
        }
        else if (SaveManager.instance.activeSave.worldsUnlocked == 3)
        {
            Aeon.transform.Find("Line").GetComponent<SpriteRenderer>().enabled = true;
            Aeon.GetComponent<SpriteRenderer>().color = Color.white;
            Dor.transform.Find("Line").GetComponent<SpriteRenderer>().enabled = true;
            Dor.GetComponent<SpriteRenderer>().color = Color.white;
            currentWorldsUnlocked = 3;
        }
    }
    
}