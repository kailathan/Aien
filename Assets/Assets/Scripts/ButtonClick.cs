using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button thisButton;
    private void Start()
    {
        thisButton.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        print("clickety");
    }
}
