using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    private bool inCombat;
    private Vector2 bounds;
    public GameObject virtualCam;

    private void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            virtualCam.GetComponent<CinemachineVirtualCamera>().Priority = 20;
            inCombat = true;
        }
    }
    private void LateUpdate()
    {
        if (inCombat == true)
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, bounds.x, bounds.x * -1);
            viewPos.y = Mathf.Clamp(viewPos.y, bounds.y, bounds.y * -1);
            transform.position = viewPos;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            virtualCam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        }
    }
}

