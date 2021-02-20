using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tooltip : MonoBehaviour
{
    private GameObject textToDisplay;
    [SerializeField] private Vector2 offset;
    private bool gameObjectEnabled = true;
    private void Awake()
    {
        textToDisplay = transform.Find("Background").gameObject;
        offset = new Vector2(300, 0);
        HideToolTip();
    }
    public void ShowToolTip()
    {
        textToDisplay.SetActive(true);
        gameObjectEnabled = true;
        //textToDisplay.transform.localPosition = new Vector3(300, 0, textToDisplay.transform.position.z);
    } 
    public void HideToolTip()
    {
        if (gameObjectEnabled)
        {
            textToDisplay.SetActive(false);
            gameObjectEnabled = false;
        }
    }
}
