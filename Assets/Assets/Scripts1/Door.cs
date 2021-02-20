using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour

{
    public GameObject HouseCam;
    private bool isAtDoor;
    private void Start()
    {
        HouseCam = GameObject.FindGameObjectWithTag("House");

    }
    // Update is called once per frame
    void Update()
    {
        if(isAtDoor == true && Input.GetKeyDown(KeyCode.W))
        {
            Transform t = GameObject.Find("Player").transform;
            t.position = new Vector2(1, 0);
            HouseCam.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            isAtDoor = true;

        }
    }
}
